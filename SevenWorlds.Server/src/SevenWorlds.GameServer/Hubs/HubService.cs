using Microsoft.AspNet.SignalR;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;

namespace SevenWorlds.GameServer.Hubs
{
    public class HubService : IHubService
    {
        private readonly IHubContext hubContext;

        public HubService()
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
        }

        public void PingAll()
        {
            hubContext.Clients.All.OnPing(new PingData());
        }

        public void AddPlayerToAreaGroup(PlayerData playerData, AreaData areaData)
        {
            hubContext.Groups.Add(playerData.ConnectionId, areaData.Id);
        }

        public void RemovePlayerFromAreaGroup(PlayerData playerData, AreaData areaData)
        {
            hubContext.Groups.Remove(playerData.ConnectionId, areaData.Id);
        }

        public void AreaSync(AreaSyncData data)
        {
            hubContext.Clients.Group(data.Area.Id).OnAreaSync(data);
        }

        public void ReturnActionStatusToPlayer(string playerId, PlayerActionStatusData data)
        {

        }
    }
}
