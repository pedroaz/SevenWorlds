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
        public WorldPosition FromPosition { get; set; }
        public WorldPosition ToPosition { get; set; }
    }
}
