using Microsoft.Owin.Hosting;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.Equipment;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Quests;
using SevenWorlds.GameServer.Gameplay.Talent;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
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
        private readonly ISkillFactory skillFactory;
        private readonly IEquipmentFactory equipmentFactory;
        private readonly ITalentFactory talentFactory;
        private readonly IQuestFactory questFactory;

        private ILogService logService { get; }
        private IGameFactory gameFactory { get; }
        private IGameLoopSimulator gameLoopSimulator { get; }
        private volatile GameServerStatus serverStatus;

        public ServerManager(ILogService logService, IGameLoopSimulator gameLoopSimulator,
            IGameFactory gameFactory, IConfigurator configurator, IMonsterDataFactory monsterDataFactory,
            ISkillFactory skillFactory, IEquipmentFactory equipmentFactory, ITalentFactory talentFactory, IQuestFactory questFactory)
        {
            this.logService = logService;
            this.gameFactory = gameFactory;
            this.configurator = configurator;
            this.monsterDataFactory = monsterDataFactory;
            this.skillFactory = skillFactory;
            this.equipmentFactory = equipmentFactory;
            this.talentFactory = talentFactory;
            this.questFactory = questFactory;
            this.gameLoopSimulator = gameLoopSimulator;
        }

        public async Task StartServer()
        {
            try {
                logService.Log("Starting the Game Server", type: LogType.Initialization);
                using (WebApp.Start(NetworkConstants.ServerUrl)) {
                    logService.Log($"Server running on {NetworkConstants.ServerUrl}", type: LogType.Initialization);

                    SetupStorages();

                    if (configurator.Config.AutoStart) {
                        logService.Log($"Server is configured to auto start and it will start with ServerId from config file: {configurator.Config.ServerId}", type: LogType.Initialization);
                        serverStatus = GameServerStatus.ReadyToStart;
                    }
                    else {
                        serverStatus = GameServerStatus.WaitingForStartRequest;
                        logService.Log($"Server is not configured to auto start. Waiting for the StartGameServer request", type: LogType.Initialization);
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

        private void SetupStorages()
        {
            logService.Log("Setting up Skill factory", type: LogType.Initialization);
            skillFactory.SetupStorage();
            logService.Log("Setting up Monster factory", type: LogType.Initialization);
            monsterDataFactory.SetupStorage();
            logService.Log("Setting up Equipment factory", type: LogType.Initialization);
            equipmentFactory.SetupStorage();
            logService.Log("Setting up Talent factory", type: LogType.Initialization);
            talentFactory.SetupStorage();
            logService.Log("Setting up Quest factory", type: LogType.Initialization);
            questFactory.SetupStorage();
        }

        private async Task StartGameServer(string serverId)
        {
            if (serverStatus == GameServerStatus.ReadyToStart) {

                try {
                    if (serverId.Equals(string.Empty)) {
                        logService.Log("ServerId is empty - using default server id: fake_server", type: LogType.Initialization);
                        serverId = "fake_server";
                    }

                    serverStatus = GameServerStatus.Initializing;
                    Task initTask = InitializeGameServer(serverId);
                    initTask.Wait();
                }
                catch (AggregateException agg) {

                    logService.Log("Error on initialization. Server is now faulted", type: LogType.Initialization, level: LogLevel.Error);

                    foreach (Exception e in agg.InnerExceptions) {
                        logService.Log(e.Message);
                    }

                    serverStatus = GameServerStatus.Faulted;
                    await Task.Delay(Timeout.Infinite);
                }

                logService.Log("----------------------------", type: LogType.Initialization);
                logService.Log("------- SERVER_START -------", type: LogType.Initialization);
                logService.Log("----------------------------", type: LogType.Initialization);
                serverStatus = GameServerStatus.Started;
                gameLoopSimulator.StartSimulation();
            }
        }

        private async Task InitializeGameServer(string serverId)
        {
            logService.Log($"Starting game server with serverId: {serverId}", type: LogType.Initialization);
            await gameFactory.SetupGameServer(serverId);
            gameFactory.DumpMasterData();
        }

        private async Task WaitForStartRequest()
        {
            logService.Log("Waiting for Start Request", type: LogType.Initialization);
            while (serverStatus != GameServerStatus.ReadyToStart) {
                await Task.Delay(1000);
            }
        }

        public void StartServerRequest(string serverId)
        {
            logService.Log("Setting server status to Ready to start", type: LogType.Initialization);
            configurator.Config.ServerId = serverId;
            serverStatus = GameServerStatus.ReadyToStart;
        }

        public async Task ResetFakeData()
        {

            await gameFactory.SetFakeData();
        }
    }
}
