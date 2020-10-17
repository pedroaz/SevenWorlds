using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Movement
{
    public class MovementActionParameters : PlayerActionParameters
    {
        public string FromAreaId { get; set; }
        public string ToAreaId { get; set; }
        public string CharacterId { get; set; }
        public WorldPosition Position{ get; set; }

        public MovementActionParameters(PlayerActionData data) : base(data)
        {

        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
