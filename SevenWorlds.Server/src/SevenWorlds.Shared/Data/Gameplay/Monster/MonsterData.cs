using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum MonsterType
    {
        Poring,
        PecoPeco
    }

    public class MonsterData : NetworkData
    {
        MonsterType MonsterType;
        public HpData HpData { get; set; }
        public CombatData CombatData { get; set; }
    }
}
