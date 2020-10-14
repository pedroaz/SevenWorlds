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

        #region Groups

        public void AddPlayerToAreaGroup(string playerConnectionId, string areaId)
        {
            hubContext.Groups.Add(playerConnectionId, areaId);
        }

        public void RemovePlayerFromAreaGroup(string playerConnectionId, string areaId)
        {
            hubContext.Groups.Remove(playerConnectionId, areaId);
        }

        #endregion

        #region Broadcasts

        public void BroadcastPing()
        {
            hubContext.Clients.All.OnPing(new PingData());
        }

        public void BroadcastAreaSync(AreaSyncData data)
        {
            hubContext.Clients.Group(data.Area.ObjectId).OnAreaSync(data);
        }

        #endregion
    }
}
