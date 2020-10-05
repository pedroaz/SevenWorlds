using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Network;
using System;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server.Manager
{
    public class ServerManager : IServerManager
    {
        private ILogService logService { get; }
        private IUniverseFactory universeFactory { get; }
        private IGameLoopSimulator gameLoopSimulator { get; }

        public ServerManager(ILogService logService, IGameLoopSimulator gameLoopSimulator, IUniverseFactory universeFactory)
        {
            this.logService = logService;
            this.universeFactory = universeFactory;
            this.gameLoopSimulator = gameLoopSimulator;
        }

        public async Task StartServer()
        {
            try {
                logService.Log("Starting the Game Server");
                using (WebApp.Start(NetworkConstants.ServerUrl)) {
                    logService.Log($"Server running on {NetworkConstants.ServerUrl}");
                    universeFactory.SetupFakeUniverses();
                    gameLoopSimulator.StartSimulation();
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
