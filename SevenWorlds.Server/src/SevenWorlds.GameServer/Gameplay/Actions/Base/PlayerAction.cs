using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerAction
    {
        internal IGameStateService gameStateService;
        internal IHubService hubService;
        internal LoopSyncCoordinator syncCoordinator;

        public PlayerAction()
        {

        }

        public PlayerAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService, LoopSyncCoordinator syncCoordinator)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
            this.syncCoordinator = syncCoordinator;
        }

        public virtual void Execute()
        {

        }
    }
}
