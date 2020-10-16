using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.PlayerActions.Datas
{
    public class CreateBattleEncounterActionData : PlayerActionData
    {
        public string CharacterId { get; set; }
        public string SectionId { get; set; }

        public CreateBattleEncounterActionData()
        {
            ActionType = PlayerActionType.CreateBattleEncounter;
        }
    }
}
