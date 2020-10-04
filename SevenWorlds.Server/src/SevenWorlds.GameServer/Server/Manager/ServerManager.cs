using SevenWorlds.GameServer.Utils.Log;
using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using SevenWorlds.Shared.Network;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.Shared.Data;
using System.Threading;

namespace SevenWorlds.GameServer.Server.Manager
{
    public class ServerManager : IServerManager
    {
        private ILogService logService { get; }

        public ServerManager(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task StartServer()
        {
            try {
                logService.Log("Starting the Game Server");
                using (WebApp.Start(NetworkConstants.ServerUrl)) {
                    logService.Log($"Server running on {NetworkConstants.ServerUrl}");
                    Thread.Sleep(Timeout.Infinite);
                }

            }
            catch (Exception e) {
                logService.Log(e.Message);
                throw;
            }

            
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.UseCors(CorsOptions.AllowAll);
                app.MapSignalR();
            }
        }

        [HubName(NetworkConstants.MainHubName)]
        public class MainHub : Hub
        {
            public void SendChatMessage(ChatMessageData data)
            {
                System.Diagnostics.Debug.WriteLine("Recieved Chat Message Command");
                Clients.All.OnChatMessage(data);
            }
        }
    }
}
