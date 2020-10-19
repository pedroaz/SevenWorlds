using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum CharacterResourceType
    {
        Gold,
        Rock,
        Wood
    }

    public class CharacterResourcesData : NetworkData
    {
        public Dictionary<CharacterResourceType, int> Resources { get; set; }
    }
}
