using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class CombatData
    {
        public int MaxHp { get; set; }
        public int CurrentHp { get; set;  }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public int Fire { get; set; }
        public int Water { get; set; }
        public int Earth { get; set; }
        public int Air { get; set; }

        // List of skills
    }
}
