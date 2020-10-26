using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum AreaType
    {
        City,
        Battleground
    }

    [System.Serializable]
    public class AreaData : NetworkData
    {
        public string Name;
        public Position Position;
        public string WorldId;
        public AreaType Type;

        public AreaData(string name, Position position, string worldId, AreaType type)
        {
            Name = name;
            Position = position;
            WorldId = worldId;
            Type = type;
        }
    }
}
