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
        private Queue<PlayerActionData> playerActionDatas;

        private static object queueLock = new object();

        public PlayerActionQueue()
        {
            playerActionDatas = new Queue<PlayerActionData>();
        }

        public PlayerActionStatusData AddToQueue(PlayerActionData playerAction)
        {
            lock (queueLock) {
                playerActionDatas.Enqueue(playerAction);
            }

            return new PlayerActionStatusData();
        }
        public PlayerActionStatusData GetStatusByActionId(string actionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerActionData> GetAllFromQueue()
        {
            return playerActionDatas;
        }
    }
}
