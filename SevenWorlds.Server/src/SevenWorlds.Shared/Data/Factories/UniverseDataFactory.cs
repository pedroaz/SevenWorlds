using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Factory
{
    public class UniverseDataFactory : DataFactory
    {
        public UniverseData CreateNewUniverse(string name)
        {
            var data = new UniverseData(name);
            SetDefaultValues(data);
            return data;
        }

        public WorldData CreateNewWorld(string name, string universeId, int index)
        {
            var data = new WorldData(name, universeId, index);
            SetDefaultValues(data);
            return data;
        }

        public AreaData CreateNewArea(string name, Position position, string worldId)
        {
            var data = new AreaData(name, position, worldId);
            SetDefaultValues(data);
            return data;
        }

        public MonsterCampData CreateNewMonsterCamp(MonsterType monsterType)
        {
            var data = new MonsterCampData(monsterType);
            SetDefaultValues(data);
            return data;
        }

        public ProductionCampData CreateNewProductionCamp(WorldResourceType resourceType)
        {
            var data = new ProductionCampData(resourceType);
            SetDefaultValues(data);
            return data;
        }

        public SectionBundle CreateNewSectionBundle()
        {
            var data = new SectionBundle(){ 
                Armories = new List<ArmoryData>(),
                MonsterCamps = new List<MonsterCampData>(),
                ProductionCamps = new List<ProductionCampData>(),
                Shops = new List<ShopData>()
            };
            SetDefaultValues(data);
            return data;
        }
    }
}
