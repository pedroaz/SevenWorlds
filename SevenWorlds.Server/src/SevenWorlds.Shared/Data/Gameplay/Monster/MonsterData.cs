using SevenWorlds.Shared.Data.Base;
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
        public CombatData Combat { get; set; }
    }
}
