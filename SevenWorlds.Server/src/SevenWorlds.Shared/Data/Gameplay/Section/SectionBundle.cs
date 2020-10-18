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
        public List<MonsterCampData> Armories { get; set; } = new List<MonsterCampData>();
        public List<MonsterCampData> Shops { get; set; } = new List<MonsterCampData>();
        public List<MonsterCampData> ProductionCamps { get; set; } = new List<MonsterCampData>();
    }
}
