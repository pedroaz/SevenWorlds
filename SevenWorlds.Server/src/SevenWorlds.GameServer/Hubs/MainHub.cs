using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.Shared.Data;
using SevenWorlds.Shared.Network;
using System;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        public void SendChatMessage(ChatMessageData data)
        {
            System.Diagnostics.Debug.WriteLine("Recieved Chat Message Command");
            // Call the broadcastMessage method to update clients.
            Clients.All.OnChatMessage(data);
        }
    }
}