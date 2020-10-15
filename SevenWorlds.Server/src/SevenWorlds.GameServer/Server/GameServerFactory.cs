using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public class GameServerFactory : IGameServerFactory
    {
        private readonly IDatabaseService databaseService;
        private readonly ILogService logService;
        private readonly IConfigurator configurator;
        private readonly IUniverseCollection universeCollection;
        private readonly IWorldCollection worldCollection;
        private readonly IAreaCollection areaCollection;
        private readonly ISectionCollection sectionCollection;

        public GameServerFactory(
            IUniverseCollection universeCollection, 
            IWorldCollection worldCollection, 
            IAreaCollection areaCollection, 
            ISectionCollection sectionCollection,
            IDatabaseService databaseService,
            ILogService logService,
            IConfigurator configurator)
        {
            this.universeCollection = universeCollection;
            this.worldCollection = worldCollection;
            this.areaCollection = areaCollection;
            this.sectionCollection = sectionCollection;
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
            foreach (var item in masterData.UniverseCollection) {
                universeCollection.Add(item);
            }


            // Add Worlds
            foreach (var item in masterData.WorldCollection) {
                worldCollection.Add(item);
            }

            // Add Areas
            foreach (var item in masterData.AreaCollection) {
                areaCollection.Add(item);
            }

            // Add Sections
            foreach (var item in masterData.SectionCollection) {
                sectionCollection.Add(item);
            }
        }

        public async Task SetFakeData()
        {

            // Set Accounts
            // Set Master Data

            var universe = new UniverseData() {
                Name = "First Universe"
            };

            var world0 = new WorldData() {
                Name = "World 1",
                UniverseId = universe.Id,
                WorldIndex = 0
            };

            var world1 = new WorldData() {
                Name = "World 2",
                UniverseId = universe.Id,
                WorldIndex = 1
            };

            var world2 = new WorldData() {
                Name = "World 3",
                UniverseId = universe.Id,
                WorldIndex = 2
            };

            var world3 = new WorldData() {
                Name = "World 4",
                UniverseId = universe.Id,
                WorldIndex = 3
            };

            var world4 = new WorldData() {
                Name = "World 5",
                UniverseId = universe.Id,
                WorldIndex = 4
            };

            var world5 = new WorldData() {
                Name = "World 6",
                UniverseId = universe.Id,
                WorldIndex = 5
            };

            var world6 = new WorldData() {
                Name = "World 7",
                UniverseId = universe.Id,
                WorldIndex = 6
            };

            var firstArea = new AreaData() {
                Name = "First Area",
                Position = new WorldPosition() {
                    X = 0,
                    Y = 0
                },
                WorldId = world0.Id
            };

            var secondArea = new AreaData() {
                Name = "Second Area",
                Position = new WorldPosition() {
                    X = 1,
                    Y = 0
                },
                WorldId = world0.Id
            };

            var section1 = new SectionData() {
                Name = "Poring Camp",
                AreaId = firstArea.Id,
                SectionType = SectionTypes.MonsterCamp
            };


            MasterDataModel masterData = new MasterDataModel() {
                ServerId = "fake_server",
                UniverseCollection = new List<UniverseData>() {
                    universe
                },
                WorldCollection = new List<WorldData>() {
                    world0, world1, world2, world3,
                    world4, world5, world6
                },
                AreaCollection = new List<AreaData>() {
                    firstArea, secondArea
                },
                SectionCollection = new List<SectionData>() {
                    section1
                }
            };

            await databaseService.UpdateMasterData(masterData);
        }

    }
}
