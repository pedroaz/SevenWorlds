using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    public class SectionBundle
    {
        public List<MonsterCampData> MonsterCamps { get; set; } = new List<MonsterCampData>();
        public List<ArmoryData> Armories { get; set; } = new List<ArmoryData>();
        public List<ShopData> Shops { get; set; } = new List<ShopData>();
        public List<ProductionCampData> ProductionCamps { get; set; } = new List<ProductionCampData>();
    }
}
