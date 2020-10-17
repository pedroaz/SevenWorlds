using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class CreateBattleEncounterAction : PlayerAction
    {
        public CreateBattleEncounterAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService) : base(data, gameStateService, hubService)
        {

        }

        public override void OnExecute()
        {
            base.OnExecute();
        }


    }
}
