using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public class UniverseFactory : IUniverseFactory
    {
        private IUniverseCollection universeCollection { get; }
        private IWorldCollection worldCollection { get; }
        private IAreaCollection areaCollection { get; }
        private ISectionCollection sectionCollection { get; }

        public UniverseFactory(
            IUniverseCollection universeCollection, 
            IWorldCollection worldCollection, 
            IAreaCollection areaCollection, 
            ISectionCollection sectionCollection)
        {
            this.universeCollection = universeCollection;
            this.worldCollection = worldCollection;
            this.areaCollection = areaCollection;
            this.sectionCollection = sectionCollection;
        }

        public void SetupFakeUniverses()
        {
            var universe = new UniverseData() {
                Name = "First Universe"
            };

            var world = new WorldData() {
                Name = "First World",
                UniverseId = universe.Id
            };

            var area = new AreaData() {
                Name = "First Area",
                Position = new WorldPosition() {
                    X = 0,
                    Y = 0
                },
                WorldId = world.Id
            };

            var section = new SectionData() {
                Name = "Poring Camp",
                AreaId = area.Id,
                SectionType = SectionTypes.MonsterCamp
            };

            universeCollection.Add(universe);
            worldCollection.Add(world);
            areaCollection.Add(area);
            sectionCollection.Add(section);
        }
    }
}
