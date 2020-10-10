using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class PlayerMovementAction : PlayerAction
    {
        private readonly PlayerMovementActionData data;

        public PlayerMovementAction(PlayerMovementActionData data, 
            IGameStateService gameStateService, IHubService hubService) : 
            base(data, gameStateService, hubService)
        {
            Scale = PlayerActionScale.Area;
            this.data = data;
        }

        public override void Simulate()
        {
            gameStateService.MovePlayerToArea(data.Id, data.ToAreaId);
        }

        public override void End()
        {
            var player = gameStateService.PlayerCollection.FindById(data.PlayerId);
            var area = gameStateService.AreaCollection.FindById(data.ToAreaId);

            if (player.AreaId != null) {
                hubService.RemovePlayerFromAreaGroup(player.ConnectionId, area.Id);
            }
            hubService.AddPlayerToAreaGroup(player.ConnectionId, area.Id);

            hubService.BroadcastAreaSync(gameStateService.GetAreaSyncData(data.FromAreaId));
            hubService.BroadcastAreaSync(gameStateService.GetAreaSyncData(data.ToAreaId));
        }
    }
}
