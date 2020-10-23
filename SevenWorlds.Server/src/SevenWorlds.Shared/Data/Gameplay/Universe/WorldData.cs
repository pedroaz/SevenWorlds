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
        public string Name;
        public string UniverseId;
        public int WorldIndex;

        public WorldData(string name, string universeId, int worldIndex)
        {
            Name = name;
            UniverseId = universeId;
            WorldIndex = worldIndex;
        }

      
    }
}
