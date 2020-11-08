using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Base
{
    public class PlayerActionCollection : IPlayerActionCollection
    {
        PlayerActionBundle bundle;

        private static object queueLock = new object();
        private readonly ILogService logService;

        public PlayerActionCollection(ILogService logService)
        {
            bundle = new PlayerActionBundle();
            this.logService = logService;
        }

        public void AddToBundle(MovementActionData action)
        {
            logService.Log($"Adding Movement Action to queue: {action.CharacterId}");
            lock (queueLock) {
                bundle.Movement.Add(action);
            }
        }

        public PlayerActionBundle CopyActionCollection()
        {
            lock (queueLock) {

                PlayerActionBundle bundleCopy = new PlayerActionBundle(){ 
                    Movement = bundle.Movement,
                };

                bundle = new PlayerActionBundle();
            
                return bundleCopy;
            }
        }
    }
}
