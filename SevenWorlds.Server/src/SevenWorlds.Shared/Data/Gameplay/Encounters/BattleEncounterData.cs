using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Encounters
{
    public class BattleEncounterData : EncounterData
    {
        public List<string> charaters { get; set; }
        public MonsterData monster { get; set; }
        public int MaxAmountOfCharacters { get; set; }
    }
}
