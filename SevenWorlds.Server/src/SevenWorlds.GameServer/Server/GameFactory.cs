using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public class GameFactory : IGameFactory
    {
        private readonly IDatabaseService databaseService;
        private readonly ILogService logService;
        private readonly IConfigurator configurator;
        private readonly ICharacterFactory characterFactory;
        private readonly IGameStateService gameStateService;
        private readonly UniverseDataFactory universeDataFactory = new UniverseDataFactory();

        public GameFactory(
            IGameStateService gameStateService,
            IDatabaseService databaseService,
            ILogService logService,
            IConfigurator configurator,
            ICharacterFactory characterFactory)
        {
            this.gameStateService = gameStateService;
            this.databaseService = databaseService;
            this.logService = logService;
            this.configurator = configurator;
            this.characterFactory = characterFactory;
        }

        public void DumpMasterData()
        {
            if (configurator.Config.MasterDataDumpFolder.Equals(string.Empty)) {
                logService.Log("Dump will no be done because dump folder isn't valid");
            }

            MasterDataModel masterData = gameStateService.GetMasterData();

            var jsonText = JsonConvert.SerializeObject(masterData, Formatting.Indented);
            var fileName = Path.Combine(configurator.Config.MasterDataDumpFolder, $"{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}_MASTER_DUMP.json");

            logService.Log($"Dumping master data to file: {fileName}");

            try {
                File.WriteAllText(fileName, jsonText);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }

            logService.Log($"Finishded dumping to file: {fileName}");
        }

        public async Task SetupGameServer(string serverId)
        {
            logService.Log($"Setting up Game Server with id: {serverId}");
            MasterDataModel masterData = await databaseService.GetMasterData(serverId);
            if (masterData == null) {
                logService.Log("Master Data is null!");
                throw new AggregateException("Master Data is null");
            }

            // Add universes
            foreach (var item in masterData.Universes) {
                gameStateService.UniverseCollection.Add(item);
            }

            // Add Worlds
            foreach (var item in masterData.Worlds) {
                gameStateService.WorldCollection.Add(item);
            }

            // Add Areas
            foreach (var item in masterData.Areas) {
                gameStateService.AreaCollection.Add(item);
            }

            //Add Sections
            gameStateService.SectionCollection.SetBundle(masterData.Sections);
        }

        public string NewId()
        {
            return Guid.NewGuid().ToString();
        }

        #region Fake


        public async Task SetFakeData()
        {
            logService.Log("Setting fake data");
            logService.Log("Deleting all");
            await databaseService.DeleteAll();

            logService.Log("Creating master data");
            var masterData = GenerateFakeMasterData();

            logService.Log("Updating Databases");
            logService.Log("Updating Master Data");
            await databaseService.InsertMasterData(masterData);

            logService.Log("Finish updating databases");
        }



        private List<AccountModel> GenerateFakeAccounts()
        {
            return new List<AccountModel>() {
                new AccountModel() {
                    PlayerName = "Pedro",
                    Username = "1",
                    Password = "1",
                },
                new AccountModel() {
                    PlayerName = "Carol",
                    Username = "2",
                    Password = "2",
                },
            };
        }

        private List<PlayerModel> GenerateFakePlayers()
        {
            return new List<PlayerModel>() {
                new PlayerModel() {
                    PlayerName = "Pedro",
                    Data = new PlayerData("Pedro") {
                    }
                }
            };
        }


        private MasterDataModel GenerateFakeMasterData()
        {
            List<UniverseData> universes = CreateNewUniverse("First Universe");
            List<WorldData> worlds = CreateSevenWorlds(universes[0]);
            List<AreaData> areas = CreateFakeAreas(worlds[0]);
            SectionBundle bundle = CreateFakeSectionBundle(areas[0]);

            MasterDataModel masterData = new MasterDataModel() {
                ServerId = "fake_server",
                Universes = universes,
                Worlds = worlds,
                Areas = areas,
                Sections = bundle,
            };
            return masterData;
        }


        private SectionBundle CreateFakeSectionBundle(AreaData areaData)
        {
            SectionBundle bundle = universeDataFactory.CreateNewSectionBundle();

            bundle.MonsterCamps.Add(universeDataFactory.CreateNewMonsterCamp(MonsterType.Poring));
            bundle.MonsterCamps.Add(universeDataFactory.CreateNewMonsterCamp(MonsterType.PecoPeco));
            bundle.ProductionCamps.Add(universeDataFactory.CreateNewProductionCamp(WorldResourceType.Wood));

            return bundle;
        }

        private List<AreaData> CreateFakeAreas(WorldData world)
        {
            List<AreaData> areas = new List<AreaData>();

            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {

                    var areaType = AreaType.Battleground;

                    if (x == 0 && y == 0) {
                        areaType = AreaType.City;
                    }


                    areas.Add(
                        universeDataFactory.CreateNewArea(
                            $"Area ({x},{y})",
                            new Position(x, y),
                            world.Id,
                            areaType
                        )
                    );
                }
            }

            return areas;
        }

        private List<WorldData> CreateSevenWorlds(UniverseData universe)
        {
            List<WorldData> worlds = new List<WorldData>();

            for (int i = 0; i < 7; i++) {
                worlds.Add(
                    universeDataFactory.CreateNewWorld(
                        $"World {i}",
                        universe.Id,
                        i
                    )
                );
            }

            return worlds;
        }

        private List<UniverseData> CreateNewUniverse(string universeName)
        {
            return new List<UniverseData>(){
                universeDataFactory.CreateNewUniverse(universeName)
            };
        }

        #endregion


    }
}
