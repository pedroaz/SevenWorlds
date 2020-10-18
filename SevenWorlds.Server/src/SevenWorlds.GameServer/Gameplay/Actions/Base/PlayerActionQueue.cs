using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerActionQueue : IPlayerActionQueue
    {
        PlayerActionBundle bundle;

        private static object queueLock = new object();

        public PlayerActionQueue()
        {
            bundle = new PlayerActionBundle();
        }

        public void AddToQueue(MovementActionData action)
        {
            lock (queueLock) {
                bundle.Movement.Enqueue(action);
            }
        }

        public void AddToQueue(StartBattleActionData action)
        {
            lock (queueLock) {
                bundle.StartBattle.Enqueue(action);
            }
        }

        public PlayerActionBundle CopyActionCollection()
        {
            lock (queueLock) {
                PlayerActionBundle bundleCopy = new PlayerActionBundle();

                foreach (var item in bundleCopy.Movement) {
                    bundleCopy.Movement.Enqueue(bundle.Movement.Dequeue());
                }

                foreach (var item in bundleCopy.StartBattle) {
                    bundleCopy.StartBattle.Enqueue(bundle.StartBattle.Dequeue());
                }
            
                return bundleCopy;
            }
        }
    }
}
