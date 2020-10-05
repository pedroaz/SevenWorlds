using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Network;
using System;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;

        public MainHub(ILogService logService, IGameStateService gameStateService)
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
    }
}
