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
        public PlayerActionStatus Status { get; set; }
        public string ActionId { get; set; }
        internal IGameStateService gameStateService;
        internal IHubService hubService;

        public PlayerAction(PlayerActionData data, IGameStateService gameStateService, IHubService hubService)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
            Status = PlayerActionStatus.Started;
            ActionId = data.Id;
            OnStart();
        }
        public void Simulate()
        {
            Status = PlayerActionStatus.Ongoing;
            OnSimulate();
        }

        public void Finish()
        {
            Status = PlayerActionStatus.Finished;
            OnFinish();
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnSimulate()
        {
        }

        public virtual void OnFinish()
        {
        }
    }
}
