using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SevenWorlds.Shared.Data.Gameplay.SectionData;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public class GameServerFactory : IGameServerFactory
    {
        private readonly IDatabaseService databaseService;
        private readonly ILogService logService;
        private readonly IConfigurator configurator;
        private readonly IGameStateService gameStateService;

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

            MasterDataModel masterData = new MasterDataModel(){ 
                ServerId = "123"
            };

            var jsonText = JsonConvert.SerializeObject(masterData, Formatting.Indented);
            var fileName = Path.Combine(configurator.GetMasterDataDumpFoler(),$"{DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss")}_MASTER_DUMP.json");

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
            if(masterData == null) {
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

            // Add Sections
            //foreach (var item in masterData.SectionCollection) {
            //    gameStateService.SectionCollection.Add(item);
            //}
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
            await databaseService.UpdateCharacter(GenerateFakeCharacters(masterData.Worlds));
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

        private CharacterModel GenerateFakeCharacters(List<WorldData> worlds)
        {
            return new CharacterModel() {
                data = new CharacterData() {
                    Id = NewId(),
                    Level = 0,
                    PlayerName = "Pedro",
                    WorldId = worlds[0].Id,
                    Position = new WorldPosition() {
                        X = 0,
                        Y = 0
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
                Encounters = new EncounterBundle()
            };
            return masterData;
        }

        private SectionBundle CreateFakeSectionBundle(AreaData areaData)
        {
            SectionBundle bundle = new SectionBundle();

            bundle.MonsterCamps.Add(new MonsterCampData() {
                Name = "Poring Camp",
                AreaId = areaData.Id,
                sectionType = SectionType.MonsterCamp,
                Id = NewId()
            });

            return bundle;
        }

        private List<AreaData> CreateFakeAreas(WorldData world)
        {
            List<AreaData> areas = new List<AreaData>();

            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {
                    areas.Add(new AreaData() {
                        Id = NewId(),
                        Name = $"Area ({x},{y})",
                        Position = new WorldPosition() {
                            X = x,
                            Y = y
                        },
                        WorldId = world.Id
                    });
                }
            }

            return areas;
        }

        private List<WorldData> CreateSevenWorlds(UniverseData universe)
        {
            List<WorldData> worlds = new List<WorldData>();

            for (int i = 0; i < 7; i++) {
                worlds.Add(new WorldData() {
                    Name = $"World {i}",
                    UniverseId = universe.Id,
                    WorldIndex = i,
                    Id = NewId()
                });
            }

            return worlds;
        }

        private List<UniverseData> CreateNewUniverse(string universeName)
        {
            return new List<UniverseData>(){
                new UniverseData() {
                    Name = universeName,
                    Id = NewId()
                }
            };
        }

        #endregion


    }
}
