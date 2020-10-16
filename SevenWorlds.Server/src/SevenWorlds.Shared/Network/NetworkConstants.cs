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

        // Requests - Clients call those commands on the server
        public const string Request_SendChatMessage = "RequestSendChatMessage";
        public const string Request_Login = "RequestLogin";
        public const string Request_UniverseSync = "RequestUniverseSync";
        public const string Request_WorldSync = "RequestWorldSync";
        public const string Request_AreaSync = "RequestAreaSync";
        public const string Request_PlayerAction = "RequestPlayerAction";
        public const string Request_RequestRegisterAccount = "RequestRegisterAccount";
        public const string Request_AllPlayerDatas = "RequestAllPlayerDatas";
        

        // Servers broadcast to clients
        public const string Event_OnChatMessage = "OnChatMessage";
        public const string Event_OnPing = "OnPing";
        public const string Event_OnAreaSync = "OnAreaSync";
    }
}
