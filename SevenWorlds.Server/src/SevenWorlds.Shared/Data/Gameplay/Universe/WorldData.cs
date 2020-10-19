using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class WorldData : NetworkData
    {
        public string Name { get; set; }
        public string UniverseId { get; set; }
        public int WorldIndex { get; set; }

        public WorldData(string name, string universeId, int worldIndex)
        {
            Name = name;
            UniverseId = universeId;
            WorldIndex = worldIndex;
        }

      
    }
}
