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
            universeCollection.Add(new UniverseData(){ 
                Id = 0,
                Name = "First Universe"
            });

            worldCollection.Add(new WorldData(){ 
                Id = 0,
                Name = "First World",
                UniverseId = 0
            });

            areaCollection.Add(new AreaData() {
                Id = 0,
                Name = "First Area",
                Position = new WorldPosition() {
                    X = 0,
                    Y = 0
                },
                WorldId = 0
            });

            sectionCollection.Add(new SectionData(){
                Id = 0,
                Name = "Poring Camp",
                AreaId = 0,
                SectionType = SectionTypes.MonsterCamp
            });
        }
    }
}
