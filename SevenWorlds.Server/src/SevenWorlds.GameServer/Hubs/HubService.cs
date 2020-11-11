using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System.Linq;

namespace SevenWorlds.GameServer.Hubs
{
    public class HubService : IHubService
    {
        private readonly IHubContext hubContext;
        private readonly ILogService logService;

        public HubService(ILogService logService)
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
            this.logService = logService;
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
            hubContext.Clients.All.OnPing();
        }

        public void BroadcastAreaSync(AreaSyncData data)
        {
            logService.Log($"Broadcasting Area sync to clients on group: {data.Area.Id}");
            hubContext.Clients.Group(data.Area.Id).OnAreaSync(data);
        }

        public void PlayerDataSync(PlayerData data)
        {
            hubContext.Clients.Client(data.ConnectionId).OnPlayerDataSync(data);
        }

        #endregion
    }
}
