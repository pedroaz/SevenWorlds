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
        // Admin
        public const string Request_AdminStartGameServer = "AdminStartGameServer";
        public const string Request_AdminStopGameServer = "AdminStopGameServer";
        public const string Request_ResetUniverseFakeData = "ResetUniverseFakeData";
        // Account
        public const string Request_Login = "RequestLogin";
        public const string Request_RequestRegisterAccount = "RequestRegisterAccount";
        // Syncs
        public const string Request_UniverseSync = "RequestUniverseSync";
        public const string Request_RequestAllWorlds = "RequestAllWorlds";
        public const string Request_WorldSync = "RequestWorldSync";
        public const string Request_AreaSync = "RequestAreaSync";
        public const string Request_RequestAllAreas = "RequestAllAreas";
        public const string Request_AllPlayerDatas = "RequestAllPlayerDatas";
        public const string Request_RequestAllSections = "RequestAllSections";
        public const string Request_PlayerCharacters = "RequestPlayerCharacters";
        // General
        public const string Event_OnPing = "OnPing";
        public const string Request_SendChatMessage = "RequestSendChatMessage";
        // Actions
        public const string Request_MovementAction = "RequestMovementAction";
        public const string Request_StartBattleAction = "RequestStartBattleAction";
        // Servers broadcast to clients
        public const string Event_OnChatMessage = "OnChatMessage";
        public const string Event_OnAreaSync = "OnAreaSync";
    }
}
