using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class PlayerMovementAction : PlayerAction
    {

        public PlayerMovementAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService) : 
            base(data, gameStateService, hubService)
        {

        }

        public override void OnExecute()
        {
            // Move using area id
            if(data.AreaId != null) {
                gameStateService.MovePlayerToArea(data.CharacterId, data.AreaId);
            }
            // Move using area position
            else {
                gameStateService.MovePlayerToArea(data.CharacterId, data.Position);
            }
        }
    }
}
