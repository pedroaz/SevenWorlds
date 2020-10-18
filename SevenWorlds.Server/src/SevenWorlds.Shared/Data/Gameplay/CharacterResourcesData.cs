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
        
        public CharacterResourcesData()
        {
            Resources = new Dictionary<CharacterResourceType, int>() {
                { CharacterResourceType.Gold, 0 },
                { CharacterResourceType.Rock, 0 },
                { CharacterResourceType.Wood, 0 },
            };
        }
    }
}
