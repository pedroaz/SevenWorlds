using SevenWorlds.GameServer.Gameplay.Actions.Movement;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class PlayerMovementAction : PlayerAction
    {
        private MovementActionParameters parameters;

        public PlayerMovementAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService, LoopSyncCoordinator syncCoordinator) : base(data, gameStateService, hubService, syncCoordinator)
        {
            parameters = new MovementActionParameters(data);
        }

        public override void Execute()
        {

            if (!parameters.IsValid()) return;

            // Move using area id
            if (parameters.ToAreaId != null) {
                gameStateService.MovePlayerToArea(parameters.CharacterId, parameters.ToAreaId);
            }
            // Move using area position
            else {
                gameStateService.MovePlayerToArea(parameters.CharacterId, parameters.Position);
            }

            syncCoordinator.AreasToSync.Add(parameters.ToAreaId);
        }
    }
}
