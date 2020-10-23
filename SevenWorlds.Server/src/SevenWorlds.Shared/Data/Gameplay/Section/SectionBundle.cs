using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    public class SectionBundle : NetworkData
    {
        public List<MonsterCampData> MonsterCamps;
        public List<ArmoryData> Armories;
        public List<ShopData> Shops;
        public List<ProductionCampData> ProductionCamps;
    }
}
