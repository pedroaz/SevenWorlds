using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player.Actions;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerActionFactory : IPlayerActionFactory
    {
        private readonly IGameStateService gameStateService;
        private readonly IHubService hubService;

        public PlayerActionFactory(IGameStateService gameStateService, IHubService hubService)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
        }

        public PlayerAction GenerateAction(PlayerActionData data, LoopSyncCoordinator syncCoordinator)
        {
            switch (data.ActionType) {
                case PlayerActionType.Movement:
                    return new PlayerMovementAction(data, gameStateService, hubService, syncCoordinator);
                case PlayerActionType.CreateBattleEncounter:
                    return new CreateBattleEncounterAction(data, gameStateService, hubService, syncCoordinator);
                default:
                    return new PlayerAction(data, gameStateService, hubService, syncCoordinator);
            }
        }
    }
}
