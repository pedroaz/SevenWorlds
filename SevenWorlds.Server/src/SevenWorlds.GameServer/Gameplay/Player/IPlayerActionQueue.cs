using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public interface IPlayerActionQueue
    {
        PlayerActionStatusData AddToQueue(PlayerActionData playerAction);
        PlayerActionStatusData GetStatusByActionId(string actionId);
        IEnumerable<PlayerActionData> GetAllFromQueue();
    }
}
