using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerActionQueue : IPlayerActionQueue
    {
        private Queue<PlayerActionData> playerActionQueue;

        private static object queueLock = new object();

        public PlayerActionQueue()
        {
            playerActionQueue = new Queue<PlayerActionData>();
        }

        public PlayerActionStatusData AddToQueue(PlayerActionData playerAction)
        {
            lock (queueLock) {
                playerActionQueue.Enqueue(playerAction);
            }

            return new PlayerActionStatusData();
        }
        public PlayerActionStatusData GetStatusByActionId(string actionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerActionData> GetAllFromQueue()
        {
            lock (queueLock) {
                var copy = new Queue<PlayerActionData>();
                while(playerActionQueue.Count != 0) {
                    copy.Enqueue(playerActionQueue.Dequeue());
                }
                return copy;
            }
        }
    }
}
