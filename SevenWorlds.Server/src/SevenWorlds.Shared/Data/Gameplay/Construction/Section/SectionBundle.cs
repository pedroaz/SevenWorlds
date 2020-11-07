using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    [System.Serializable]
    public class SectionBundle
    {
        public List<MonsterCampData> MonsterCamps;
        public List<ArmoryData> Armories;
        public List<ShopData> Shops;
        public List<ProductionCampData> ProductionCamps;

        public SectionBundle()
        {
            Armories = new List<ArmoryData>();
            MonsterCamps = new List<MonsterCampData>();
            ProductionCamps = new List<ProductionCampData>();
            Shops = new List<ShopData>();
        }
    }
}
