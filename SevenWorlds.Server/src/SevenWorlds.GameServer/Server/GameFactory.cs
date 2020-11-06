using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.GameServer.Gameplay.Construction.Area;
using SevenWorlds.GameServer.Gameplay.Construction.Section;
using SevenWorlds.GameServer.Gameplay.Construction.Universe;
using SevenWorlds.GameServer.Gameplay.Construction.World;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.GameServer.Utils.Rng;
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
        private readonly IRandomService randomService;
        private readonly IUniverseFactory universeFactory;
        private readonly IWorldFactory worldFactory;
        private readonly IAreaFactory areaFactory;
        private readonly ISectionFactory sectionFactory;
        private readonly IGameStateService gameStateService;

        public GameFactory(
            IGameStateService gameStateService,
            IDatabaseService databaseService,
            ILogService logService,
            IConfigurator configurator,
            IRandomService randomService,
            IUniverseFactory universeFactory,
            IWorldFactory worldFactory,
            IAreaFactory areaFactory,
            ISectionFactory sectionFactory)
        {
            this.gameStateService = gameStateService;
            this.databaseService = databaseService;
            this.logService = logService;
            this.configurator = configurator;
            this.randomService = randomService;
            this.universeFactory = universeFactory;
            this.worldFactory = worldFactory;
            this.areaFactory = areaFactory;
            this.sectionFactory = sectionFactory;
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

        public async Task LoadMasterDataFromDatabase(string serverId)
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

        #region New Universe Creation

        public async Task InsertNewMasterDataToDatabase()
        {
            logService.Log("Setting New Master Data");
            logService.Log("Deleting all");
            await databaseService.DeleteAll();

            logService.Log("Creating Master data");
            var masterData = CreateNewMasterData();

            logService.Log("Updating Databases");
            logService.Log("Updating Master Data");
            await databaseService.InsertMasterData(masterData);

            logService.Log("Finish updating databases");
        }

        private MasterDataModel CreateNewMasterData()
        {
            // Create Universe
            List<UniverseData> universes = CreateNewUniverse("Test Universe");

            // Create 7 worlds
            List<WorldData> worlds = CreateSevenWorlds(universes[0]);

            // Create areas and populate them
            List<AreaData> areas = new List<AreaData>();
            SectionBundle sections = sectionFactory.CreateNewSectionBundle();
            foreach (var world in worlds) {
                var worldAreas = CreateWorldAreas(world);
                areas.AddRange(worldAreas);
                SetSectionsIntoWorldAreas(areas, sections);
            }

            MasterDataModel masterData = new MasterDataModel() {
                ServerId = "fake_server",
                Universes = universes,
                Worlds = worlds,
                Areas = areas,
                Sections = sections,
            };
            return masterData;
        }

        private List<UniverseData> CreateNewUniverse(string universeName)
        {
            return new List<UniverseData>(){
                universeFactory.CreateNewUniverse(universeName)
            };
        }

        private List<WorldData> CreateSevenWorlds(UniverseData universe)
        {
            List<WorldData> worlds = new List<WorldData>();

            for (int i = 0; i < 7; i++) {
                worlds.Add(
                    worldFactory.CreateNewWorld(
                        $"World {i}",
                        universe.Id,
                        i
                    )
                );
            }
            return worlds;
        }

        private List<AreaData> CreateWorldAreas(WorldData world)
        {
            List<AreaData> areas = new List<AreaData>();

            int cityX = randomService.GetRandomInt(0, 9);
            int cityY = randomService.GetRandomInt(0, 9);

            for (int x = 0; x <= 9; x++) {
                for (int y = 0; y <= 9; y++) {

                    AreaType areaType;
                    if (x == cityX && y == cityY) {
                        areaType = AreaType.City;
                    }
                    else {
                        areaType = AreaType.Field;
                    }

                    areas.Add(
                        areaFactory.CreateNewArea(
                            $"Area ({x},{y})",
                            new Position(x, y),
                            world.Id,
                            areaType
                        )
                    );
                }
            }

            var city = areas.FindAll(x => x.Type == AreaType.City);

            if(city.Count != 1) {
                logService.Log("City count different from 1!", LogLevel.Error);
            }


            return areas;
        }

        private void SetSectionsIntoWorldAreas(List<AreaData> areas, SectionBundle bundle)
        {
            foreach (var area in areas) {
                if(area.Type == AreaType.City) {

                }
                else {
                    MonsterType type;

                    if (randomService.FlipCoin()) {
                        type = MonsterType.Poring;
                    }
                    else {
                        type = MonsterType.PecoPeco;
                    }

                    bundle.MonsterCamps.Add(sectionFactory.CreateNewMonsterCamp(type, area.Id));
                }
            }
        }
        #endregion


    }
}
