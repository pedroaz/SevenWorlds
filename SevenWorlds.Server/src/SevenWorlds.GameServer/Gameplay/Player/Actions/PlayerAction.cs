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
        public enum PlayerActionScale
        {
            Universe = 0,
            World = 1,
            Area = 2
        }

        public string ActionId { get; set; }
        public PlayerActionScale Scale { get; set; }
        internal IGameStateService gameStateService;
        internal IHubService hubService;

        public PlayerAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
            ActionId = data.Id;
        }

        public virtual void Simulate()
        {

        }

        public virtual void End()
        {
        }
    }
}
