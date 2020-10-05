using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
        }

        public bool Login(LoginData data)
        {
            if (gameStateService.PlayerCollection.FindByName(data.PlayerName) == null) {
                gameStateService.AddPlayerToTheGame(data, Context.ConnectionId);
                return true;
            }
            else {
                return false;
            }
        }

        public void SendChatMessage(ChatMessageData data)
        {
            logService.Log("Recieved Chat Message Command");
            Clients.All.OnChatMessage(data);
        }

        public UniverseSyncData RequestUniverseSync()
        {
            return gameStateService.GetUniverseSyncData();
        }

        public WorldSyncData RequestWorldSync(string worldId)
        {
            return gameStateService.GetWorldSyncData(worldId);
        }

        public AreaSyncData RequestAreaSync(string areaId)
        {
            return gameStateService.GetAreaSyncData(areaId);
        }
    }
}
