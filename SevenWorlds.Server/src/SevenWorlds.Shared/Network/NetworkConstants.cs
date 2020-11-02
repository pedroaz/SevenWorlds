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
        public const string Request_RegisterAccount = "RequestRegisterAccount";
        public const string Request_CreateCharacter = "RequestCreateCharacter";
        // Syncs
        public const string Request_UniverseSync = "RequestUniverseSync";
        public const string Request_AllWorlds = "RequestAllWorlds";
        public const string Request_WorldSync = "RequestWorldSync";
        public const string Request_AreaSync = "RequestAreaSync";
        public const string Request_AllAreas = "RequestAllAreas";
        public const string Request_AllPlayerDatas = "RequestAllPlayerDatas";
        public const string Request_SectionBundle = "RequestSectionBundle";
        public const string Request_PlayerCharacters = "RequestPlayerCharacters";
        public const string Request_PlayerData = "RequestPlayerData";
        public const string Request_PlayerQuests = "RequestPlayerQuests";
        // General
        public const string Request_SendChatMessage = "RequestSendChatMessage";
        // Actions
        public const string Request_MovementAction = "RequestMovementAction";
        public const string Request_StartBattleAction = "RequestStartBattleAction";
        public const string Request_StartQuest = "RequestStartQuest";
        public const string Request_CollectQuest = "RequestCollectQuest";
        // Servers Events to clients
        public const string On_ChatMessage = "OnChatMessage";
        public const string On_AreaSync = "OnAreaSync";
        public const string On_Ping = "OnPing";
        public const string On_PlayerDataSync = "OnPlayerDataSync";
    }
}
