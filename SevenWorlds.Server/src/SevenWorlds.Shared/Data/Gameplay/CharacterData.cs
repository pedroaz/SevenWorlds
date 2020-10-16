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
        public string PlayerName { get; set; }
        public int Level { get; set; }
        public string WorldId { get; set; }
    }
}
