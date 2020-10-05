using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;

namespace SevenWorlds.GameServer.Hubs
{
    public interface IHubService
    {
        void PingAll();
        void RemovePlayerFromAreaGroup(PlayerData playerData, AreaData areaData);
        void AddPlayerToAreaGroup(PlayerData playerData, AreaData areaData);
        void AreaSync(AreaSyncData data);
    }
}
