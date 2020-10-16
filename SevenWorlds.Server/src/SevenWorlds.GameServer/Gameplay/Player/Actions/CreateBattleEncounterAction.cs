using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class CreateBattleEncounterAction : PlayerAction
    {

        private readonly CreateBattleEncounterActionData data;

        public CreateBattleEncounterAction(CreateBattleEncounterActionData data,
            IGameStateService gameStateService, IHubService hubService) :
            base(data, gameStateService, hubService)
        {
            Scale = PlayerActionScale.Area;
            this.data = data;
        }

        public override void Execute()
        {
            base.Execute();
        }

        
    }
}
