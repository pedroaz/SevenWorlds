using Microsoft.Owin.Hosting;
using SevenWorlds.GameServer.Gameplay.Simulation;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factories;
using SevenWorlds.Shared.Network;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{

    public enum GameServerStatus
    {
        Initializing = 0,
        WaitingForStartRequest = 1,
        ReadyToStart = 2,
        Started = 3,
        Faulted = 4
    }

    public class ServerManager : IServerManager
    {
        private readonly IConfigurator configurator;
        private readonly IMonsterDataFactory monsterDataFactory;

        private ILogService logService { get; }
        private IGameServerFactory gameFactory { get; }
        private IGameLoopSimulator gameLoopSimulator { get; }
        private volatile GameServerStatus serverStatus;

        public ServerManager(ILogService logService, IGameLoopSimulator gameLoopSimulator, 
            IGameServerFactory gameFactory, IConfigurator configurator, IMonsterDataFactory monsterDataFactory)
        {
            this.logService = logService;
            this.gameFactory = gameFactory;
            this.configurator = configurator;
            this.monsterDataFactory = monsterDataFactory;
            this.gameLoopSimulator = gameLoopSimulator;
        }

        public async Task StartServer()
        {
            try {
                logService.Log("Starting the Game Server");
                using (WebApp.Start(NetworkConstants.ServerUrl)) {
                    logService.Log($"Server running on {NetworkConstants.ServerUrl}");

                    if (configurator.Config.AutoStart) {
                        logService.Log($"Server is configured to auto start and it will start with ServerId from config file: {configurator.Config.ServerId}");
                        serverStatus = GameServerStatus.ReadyToStart;
                    }
                    else {
                        serverStatus = GameServerStatus.WaitingForStartRequest;
                        logService.Log($"Server is not configured to auto start. Waiting for the StartGameServer request");
                        await WaitForStartRequest();
                    }

                    await StartGameServer(configurator.Config.ServerId);
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
            if (serverStatus == GameServerStatus.ReadyToStart) {

                try {
                    if (serverId.Equals(string.Empty)) {
                        logService.Log("ServerId is empty - using default server id: fake_server");
                        serverId = "fake_server";
                    }

                    serverStatus = GameServerStatus.Initializing;
                    Task initTask = InitializeGameServer(serverId);
                    initTask.Wait();
                }
                catch (AggregateException agg) {

                    logService.Log("Error on initialization. Server is now faulted");

                    foreach (Exception e in agg.InnerExceptions) {
                        logService.Log(e.Message);
                    }

                    serverStatus = GameServerStatus.Faulted;
                    await Task.Delay(Timeout.Infinite);
                }

                logService.Log("----------------------------");
                logService.Log("------- SERVER_START -------");
                logService.Log("----------------------------");
                serverStatus = GameServerStatus.Started;
                gameLoopSimulator.StartSimulation();
            }
        }

        private async Task InitializeGameServer(string serverId)
        {
            logService.Log($"Starting game server with serverId: {serverId}");
            await gameFactory.SetupGameServer(serverId);
            monsterDataFactory.SetupStorage();
            gameFactory.DumpMasterData();
        }

        private async Task WaitForStartRequest()
        {
            logService.Log("Waiting for Start Request");
            while (serverStatus != GameServerStatus.ReadyToStart) {
                await Task.Delay(1000);
            }
        }

        public void StartServerRequest(string serverId)
        {
            logService.Log("Setting server status to Ready to start");
            configurator.Config.ServerId = serverId;
            serverStatus = GameServerStatus.ReadyToStart;
        }

        public async Task ResetFakeData()
        {
            
            await gameFactory.SetFakeData();
        }
    }
}
