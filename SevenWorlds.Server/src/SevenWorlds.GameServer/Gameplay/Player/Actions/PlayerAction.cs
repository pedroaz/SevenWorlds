using SevenWorlds.GameServer.Gameplay.GameState;
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
        internal PlayerActionData data;
        internal IGameStateService gameStateService;
        internal IHubService hubService;

        public PlayerAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
            this.data = data;
        }

        public void Execute()
        {
            // TODO: Sync considering data scale?
            OnExecute();
        }

        public virtual void OnExecute()
        {

        }
    }
}
