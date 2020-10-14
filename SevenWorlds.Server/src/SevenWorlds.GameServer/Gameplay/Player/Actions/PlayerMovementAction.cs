using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class PlayerMovementAction : CharacterAction
    {
        private readonly PlayerMovementActionData data;

        public PlayerMovementAction(PlayerMovementActionData data, 
            IGameStateService gameStateService, IHubService hubService) : 
            base(data, gameStateService, hubService)
        {
            Scale = PlayerActionScale.Area;
            this.data = data;
        }

        public override void Execute()
        {
            gameStateService.MovePlayerToArea(data.ObjectId, data.ToAreaId);
        }
    }
}
