using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.ActionDatas
{
    public class MovementActionData : PlayerActionData
    {
        public string CharacterId { get; set; }
        public string FromAreaId { get; set; }
        public string ToAreaId { get; set; }

        public override PlayerActionType GetActionType => PlayerActionType.Movement;

        public MovementActionData(string characterId, string fromAreaId, string toAreaId)
        {
            CharacterId = characterId;
            FromAreaId = fromAreaId;
            ToAreaId = toAreaId;
        }
    }
}
