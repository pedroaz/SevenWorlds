using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class AreaData : NetworkData
    {
        public string Name { get; set; }
        public WorldPosition Position { get; set; }
        public int WorldId { get; set; }
    }
}
