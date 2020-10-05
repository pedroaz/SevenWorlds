using Microsoft.AspNet.SignalR.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
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

        #endregion

        #region Actions

        public void SendChatMessage(ChatMessageData data)
        {
            hubProxy.Invoke(NetworkConstants.Command_SendChatMessage, data).Wait();
        }

        public async Task<bool> Login(LoginData data)
        {
            return await hubProxy.Invoke<bool>(NetworkConstants.Command_Login, data);
        }

        #endregion
    }
}
