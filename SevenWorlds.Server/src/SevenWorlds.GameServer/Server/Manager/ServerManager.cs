using Microsoft.Owin.Hosting;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Utils.Log;
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
    }
}
