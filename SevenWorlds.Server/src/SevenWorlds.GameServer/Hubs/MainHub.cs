using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private ILogService logService;

        public MainHub(ILogService logService)
        {
            this.logService = logService;
        }

        public void SendChatMessage(ChatMessageData data)
        {
            System.Diagnostics.Debug.WriteLine("Recieved Chat Message Command");
            Clients.All.OnChatMessage(data);
        }
    }
}
