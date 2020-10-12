using Microsoft.Owin.Hosting;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Network;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public class ServerManager : IServerManager
    {
        private readonly IConfigurator configurator;

        private ILogService logService { get; }
        private IGameServerFactory gameFactory { get; }
        private IGameLoopSimulator gameLoopSimulator { get; }
        private ServerStatus serverStatus;

        public ServerManager(ILogService logService, IGameLoopSimulator gameLoopSimulator, 
            IGameServerFactory gameFactory, IConfigurator configurator)
        {
            this.logService = logService;
            this.gameFactory = gameFactory;
            this.configurator = configurator;
            this.gameLoopSimulator = gameLoopSimulator;
            serverStatus = new ServerStatus();
        }

        public async Task StartServer()
        {
            try {
                logService.Log("Starting the Game Server");
                using (WebApp.Start(NetworkConstants.ServerUrl)) {
                    logService.Log($"Server running on {NetworkConstants.ServerUrl}");

                    if (configurator.ShouldAutoStart()) {
                        logService.Log($"Server is configured to auto start and it will start with ServerId from config file: {configurator.GetServerId()}");
                        await StartGameServer(configurator.GetServerId());
                    }
                    else {
                        logService.Log($"Server is not configured to auto start. Waiting for the StartGameServer request");
                    }
                }
            }
            catch (Exception e) {
                logService.Log("Server General Exception");
                logService.Log(e.Message);
                throw;
            }
        }

        private async Task StartGameServer(string serverId)
        {
            logService.Log($"Starting game server with serverId: {serverId}");
            await gameFactory.SetupGameServer(serverId);
            gameFactory.DumpMasterData();
            gameLoopSimulator.StartSimulation();
        }

        public ServerStatus GetServerStatus()
        {
            return serverStatus;
        }
    }
}
