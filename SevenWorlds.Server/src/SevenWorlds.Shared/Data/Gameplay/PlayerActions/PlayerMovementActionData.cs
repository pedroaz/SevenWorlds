using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.PlayerActions
{
    public class PlayerMovementActionData : PlayerActionData
    {
        public string PlayerId { get; set; }
        public string FromAreaId { get; set; }
        public string ToAreaId{ get; set; }
    }
}
