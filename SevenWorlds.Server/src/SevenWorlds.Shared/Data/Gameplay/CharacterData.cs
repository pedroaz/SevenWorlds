using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class CharacterData : NetworkData
    {
        // Constant
        public string PlayerName { get; set; }

        // General
        public int Level { get; set; }
        
        // Position
        public string WorldId { get; set; }
        public string AreaId { get; set; }
        public WorldPosition Position { get; set; }

        // Combat
        public CombatData Combat { get; set; }

    }
}
