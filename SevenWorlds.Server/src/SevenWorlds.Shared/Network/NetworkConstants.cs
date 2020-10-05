using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Network
{
    public static class NetworkConstants
    {
        public const string ServerUrl = @"https://localhost:44328/";
        public const string MainHubName = "MainHub";

        // Commands - Clients call those commands on the server
        public const string Command_SendChatMessage = "SendChatMessage";
        public const string Command_Login = "Login";

        // Servers broadcast to clients
        public const string Event_OnChatMessage = "OnChatMessage";
        public const string Event_OnPing = "OnPing";
        public const string Event_OnAreaSync = "OnAreaSync";
    }
}
