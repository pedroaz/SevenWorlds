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
        PlayerActionCollection collection;

        private static object queueLock = new object();

        public PlayerActionQueue()
        {
            collection = new PlayerActionCollection();
        }

        public void AddToQueue(MovementActionData action)
        {
            lock (queueLock) {
                collection.Movement.Enqueue(action);
            }
        }

        public void AddToQueue(StartBattleActionData action)
        {
            lock (queueLock) {
                collection.StartBattle.Enqueue(action);
            }
        }

        public PlayerActionCollection CopyActionCollection()
        {
            lock (queueLock) {
                PlayerActionCollection copyCollection = new PlayerActionCollection();

                foreach (var item in copyCollection.Movement) {
                    copyCollection.Movement.Enqueue(collection.Movement.Dequeue());
                }

                foreach (var item in copyCollection.StartBattle) {
                    copyCollection.StartBattle.Enqueue(collection.StartBattle.Dequeue());
                }
            
                return copyCollection;
            }
        }
    }
}
