using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factories;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public class GameServerFactory : IGameServerFactory
    {
        private readonly IDatabaseService databaseService;
        private readonly ILogService logService;
        private readonly IConfigurator configurator;
        private readonly IGameStateService gameStateService;
        private readonly UniverseDataFactory universeDataFactory = new UniverseDataFactory();
        private readonly CharacterFactory characterFactory = new CharacterFactory();

        public GameServerFactory(
            IGameStateService gameStateService,
            IDatabaseService databaseService,
            ILogService logService,
            IConfigurator configurator)
        {
            this.gameStateService = gameStateService;
            this.databaseService = databaseService;
            this.logService = logService;
            this.configurator = configurator;
        }

        public void DumpMasterData()
        {
            if (configurator.GetMasterDataDumpFoler().Equals(string.Empty)) {
                logService.Log("Dump will no be done because dump folder isn't valid");
            }

            MasterDataModel masterData = gameStateService.GetMasterData();

            var jsonText = JsonConvert.SerializeObject(masterData, Formatting.Indented);
            var fileName = Path.Combine(configurator.GetMasterDataDumpFoler(), $"{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}_MASTER_DUMP.json");

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

            // Characters
            foreach (var item in masterData.Characters) {
                gameStateService.AddCharacterToGame(item);
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

            logService.Log("Updating databases");
            await databaseService.UpdateMasterData(masterData);
            await databaseService.UpdateAccount(GenerateFakeAccounts());
            logService.Log("Finish updating databases");
        }



        private AccountModel GenerateFakeAccounts()
        {
            return new AccountModel() {
                PlayerName = "Pedro",
                Username = "pedroaz",
                Password = "pedroaz123",
            };
        }


        private MasterDataModel GenerateFakeMasterData()
        {
            List<UniverseData> universes = CreateNewUniverse("First Universe");
            List<WorldData> worlds = CreateSevenWorlds(universes[0]);
            List<AreaData> areas = CreateFakeAreas(worlds[0]);
            SectionBundle bundle = CreateFakeSectionBundle(areas[0]);
            List<CharacterData> characters = CreateFakeCharacters(worlds[0]);

            MasterDataModel masterData = new MasterDataModel() {
                ServerId = "fake_server",
                Universes = universes,
                Worlds = worlds,
                Areas = areas,
                Sections = bundle,
                Characters = characters,
                Encounters = new EncounterBundle(),
            };
            return masterData;
        }

        private List<CharacterData> CreateFakeCharacters(WorldData world)
        {
            List<CharacterData> characters = new List<CharacterData>(){
                characterFactory.NewCharacter("Pedro", world.Id)
            };
            return characters;
        }

        private SectionBundle CreateFakeSectionBundle(AreaData areaData)
        {
            SectionBundle bundle = new SectionBundle();

            bundle.MonsterCamps.Add(universeDataFactory.CreateNewMonsterCamp(MonsterType.Poring));
            bundle.MonsterCamps.Add(universeDataFactory.CreateNewMonsterCamp(MonsterType.PecoPeco));
            bundle.ProductionCamps.Add(universeDataFactory.CreateNewProductionCamp(CharacterResourceType.Wood));

            return bundle;
        }

        private List<AreaData> CreateFakeAreas(WorldData world)
        {
            List<AreaData> areas = new List<AreaData>();

            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {

                    areas.Add(
                        universeDataFactory.CreateNewArea(
                            $"Area ({x},{y})",
                            new WorldPosition() {
                                X = x,
                                Y = y
                            },
                            world.Id
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
