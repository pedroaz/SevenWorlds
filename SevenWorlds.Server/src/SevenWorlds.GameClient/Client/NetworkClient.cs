﻿using Microsoft.AspNet.SignalR.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using System;
using System.Threading.Tasks;

namespace SevenWorlds.GameClient.Client
{
    public class NetworkClient : IDisposable
    {
        HubConnection hubConnection;
        private IHubProxy hubProxy;

        public async Task Connect(string serverUrl, string hubName)
        {
            hubConnection = new HubConnection(serverUrl);
            hubProxy = hubConnection.CreateHubProxy(hubName);
            await hubConnection.Start();
        }

        public void Dispose()
        {
            hubConnection.Dispose();
        }

        #region Set Handlers

        public void SetOnChatMessageHandler(Action<ChatMessageData> action)
        {
            hubProxy.On<ChatMessageData>(NetworkConstants.Event_OnChatMessage, (data) => {
                action(data);
            });
        }

        public void SetOnPingHandler(Action<PingData> action)
        {
            hubProxy.On<PingData>(NetworkConstants.Event_OnPing, (data) => {
                action(data);
            });
        }

        public void SetOnAreaSync(Action<AreaSyncData> action)
        {
            hubProxy.On<AreaSyncData>(NetworkConstants.Event_OnAreaSync, (data) => {
                action(data);
            });
        }

        #endregion

        #region Requests

        public async Task<ChatMessageResponse> SendChatMessage(ChatMessageData data)
        {
            return await hubProxy.Invoke<ChatMessageResponse>(NetworkConstants.Request_SendChatMessage, data);
        }

        public async Task<LoginResponseData> Login(LoginData data)
        {
            return await hubProxy.Invoke<LoginResponseData>(NetworkConstants.Request_Login, data);
        }

        public async Task<UniverseSyncData> RequestUniverseSync()
        {
            return await hubProxy.Invoke<UniverseSyncData>(NetworkConstants.Request_UniverseSync);
        }

        public async Task<WorldSyncData> RequestWorldSync(string worldId)
        {
            return await hubProxy.Invoke<WorldSyncData>(NetworkConstants.Request_WorldSync, worldId);
        }

        public async Task<AreaSyncData> RequestAreaSync(string areaId, string playerId)
        {
            return await hubProxy.Invoke<AreaSyncData>(NetworkConstants.Request_AreaSync, areaId, playerId);
        }

        public async Task<PlayerActionStatusData> RequestStartPlayerAction(PlayerActionData data)
        {
            return await hubProxy.Invoke<PlayerActionStatusData>(NetworkConstants.Request_StartPlayerAction, data);
        }

        #endregion
    }
}
