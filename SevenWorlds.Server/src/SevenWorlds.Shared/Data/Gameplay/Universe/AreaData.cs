using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class AreaData : NetworkData
    {
        public string Name;
        public WorldPosition Position;
        public string WorldId;

        public AreaData(string name, WorldPosition position, string worldId)
        {
            Name = name;
            Position = position;
            WorldId = worldId;
        }
    }
}
