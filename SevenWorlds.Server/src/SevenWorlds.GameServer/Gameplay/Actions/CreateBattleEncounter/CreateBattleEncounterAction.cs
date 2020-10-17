using SevenWorlds.GameServer.Gameplay.Actions.CreateBattleEncounter;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player.Actions
{
    public class CreateBattleEncounterAction : PlayerAction
    {
        private CreateBattleParameters parameters;

        public CreateBattleEncounterAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService, LoopSyncCoordinator syncCoordinator) : base(data, gameStateService, hubService, syncCoordinator)
        {
            parameters = new CreateBattleParameters(data);
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}
