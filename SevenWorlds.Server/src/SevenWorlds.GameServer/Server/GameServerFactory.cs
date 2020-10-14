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

        public void SetupGameServerUsingFakeData()
        {
            var universe = new UniverseData() {
                Name = "First Universe"
            };

            var world0 = new WorldData() {
                Name = "World 1",
                UniverseId = universe.ObjectId,
                WorldIndex = 0
            };

            var world1 = new WorldData() {
                Name = "World 2",
                UniverseId = universe.ObjectId,
                WorldIndex = 1
            };

            var world2 = new WorldData() {
                Name = "World 3",
                UniverseId = universe.ObjectId,
                WorldIndex = 2
            };

            var world3 = new WorldData() {
                Name = "World 4",
                UniverseId = universe.ObjectId,
                WorldIndex = 3
            };

            var world4 = new WorldData() {
                Name = "World 5",
                UniverseId = universe.ObjectId,
                WorldIndex = 4
            };

            var world5 = new WorldData() {
                Name = "World 6",
                UniverseId = universe.ObjectId,
                WorldIndex = 5
            };

            var world6 = new WorldData() {
                Name = "World 7",
                UniverseId = universe.ObjectId,
                WorldIndex = 6
            };

            var firstArea = new AreaData() {
                Name = "First Area",
                Position = new WorldPosition() {
                    X = 0,
                    Y = 0
                },
                WorldId = world0.ObjectId
            };

            var secondArea = new AreaData() {
                Name = "Second Area",
                Position = new WorldPosition() {
                    X = 1,
                    Y = 0
                },
                WorldId = world0.ObjectId
            };

            var section = new SectionData() {
                Name = "Poring Camp",
                AreaId = firstArea.ObjectId,
                SectionType = SectionTypes.MonsterCamp
            };

            universeCollection.Add(universe);
            worldCollection.Add(world0);
            worldCollection.Add(world1);
            worldCollection.Add(world2);
            worldCollection.Add(world3);
            worldCollection.Add(world4);
            worldCollection.Add(world5);
            worldCollection.Add(world6);
            areaCollection.Add(firstArea);
            areaCollection.Add(secondArea);
            sectionCollection.Add(section);
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
        }
    }
}
