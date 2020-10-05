using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class PlayerData : NetworkData
    {
        public string Name { get; set; }
        public string AreaId { get; set; }
        public string ConnectionId { get; set; }
    }
}
