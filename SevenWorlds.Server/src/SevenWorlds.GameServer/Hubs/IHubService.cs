using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;

namespace SevenWorlds.GameServer.Hubs
{
    public interface IHubService
    {
        void BroadcastPing();
        void RemovePlayerFromAreaGroup(string playerConnectionId, string areaId);
        void AddPlayerToAreaGroup(string playerConnectionId, string areaId);
        void BroadcastAreaSync(AreaSyncData data);
        void PlayerDataSync(PlayerData data);
    }
}
