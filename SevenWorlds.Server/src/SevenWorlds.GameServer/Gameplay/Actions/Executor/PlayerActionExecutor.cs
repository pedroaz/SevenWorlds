using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Executor
{
    public class PlayerActionExecutor : IPlayerActionExecutor
    {
        private readonly IGameStateService gameStateService;
        private SyncCoordinator syncCoordinator;
        private PlayerActionCollection actionCollection;

        public PlayerActionExecutor(IGameStateService gameStateService)
        {
            this.gameStateService = gameStateService;
        }

        public void SetSyncCoordinator(SyncCoordinator syncCoordinator)
        {
            this.syncCoordinator = syncCoordinator;
        }

        public void SetActionCollection(PlayerActionCollection playerActionCollection)
        {
            actionCollection = playerActionCollection;
        }


        public void ExecuteMovementActions()
        {
            foreach (var data in actionCollection.Movement) {

                gameStateService.MovePlayerToArea(data.CharacterId, data.ToAreaId);
                syncCoordinator.AreasToSync.Add(data.ToAreaId);
            }
        }

        public void ExecuteStartBattleActions()
        {
        }
    }
}
