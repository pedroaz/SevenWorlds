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
    }
}
