using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using System.Threading;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IPlayerActionQueue playerActionQueue;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService,
            IPlayerActionQueue playerActionQueue)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
        }

        public LoginResponseData RequestLogin(LoginData data)
        {
            if (gameStateService.PlayerCollection.FindByName(data.PlayerName) == null) {
                var playerData = gameStateService.AddPlayerToTheGame(data, Context.ConnectionId);
                return new LoginResponseData() {
                    UniverseSyncData = gameStateService.GetUniverseSyncData(),
                    PlayerData = playerData,
                    Success = true
                };

            }
            else {
                return new LoginResponseData() {
                    Success = false
                };
            }

        }

        public void RequestSendChatMessage(ChatMessageData data)
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

        public AreaSyncData RequestAreaSync(string areaId, string playerId)
        {
            return gameStateService.GetAreaSyncData(areaId);
        }

        public PlayerActionStatusData StartPlayerAction(PlayerActionData playerActionData)
        {
            return playerActionQueue.AddToQueue(playerActionData);
        }
    }
}
