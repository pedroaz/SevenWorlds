using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Base;
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
            await databaseService.DeleteAll();

            MasterDataModel masterData = GenerateMasterData();

            await databaseService.UpdateMasterData(masterData);
        }

        private MasterDataModel GenerateMasterData()
        {
            List<UniverseData> universes = CreateNewUniverse("First Universe");
            List<WorldData> worlds = CreateSevenWorlds(universes[0]);
            List<AreaData> areas = CreateFakeAreas(worlds[0]);
            List<SectionData> sections = CreateFakeSections(areas[0]);

            MasterDataModel masterData = new MasterDataModel() {
                ServerId = "fake_server",
                UniverseCollection = universes,
                WorldCollection = worlds,
                AreaCollection = areas,
                SectionCollection = sections
            };
            return masterData;
        }

        private List<SectionData> CreateFakeSections(AreaData areaData)
        {
            List<SectionData> sections = new List<SectionData>();

            sections.Add(new SectionData() {
                Name = "Poring Camp",
                AreaId = areaData.Id,
                SectionType = SectionTypes.MonsterCamp,
                Id = GameData.GenerateNewId()
            });
            return sections;
        }

        private List<AreaData> CreateFakeAreas(WorldData world)
        {
            List<AreaData> areas = new List<AreaData>();

            for (int x = 0; x < 2; x++) {
                for (int y = 0; y < 2; y++) {
                    areas.Add(new AreaData() {
                        Id = GameData.GenerateNewId(),
                        Name = $"Area {x+y}",
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
                    Id = GameData.GenerateNewId()
                });
            }

            return worlds;
        }

        private List<UniverseData> CreateNewUniverse(string universeName)
        {
            return new List<UniverseData>(){
                new UniverseData() {
                    Name = universeName,
                    Id = GameData.GenerateNewId()
                }
            };
        }
    }
}
