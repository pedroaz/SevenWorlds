using SevenWorlds.GameServer.Gameplay.Actions.Executor;
using SevenWorlds.GameServer.Gameplay.Encounter.Executor;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SevenWorlds.GameServer.Gameplay.Simulation
{
    public class GameLoopSimulator : IGameLoopSimulator
    {
        private ILogService logService { get; }
        private readonly IHubService hubService;
        private readonly IGameStateService gameStateService;
        private readonly IPlayerActionExecutor playerActionExecutor;
        private readonly IBattleSimulator battleSimulator;
        private readonly IPlayerActionCollection playerActionQueue;
        private Stopwatch stopwatch;
        private SyncCoordinator syncCoordinator;

        private int tickCount = 0;

        public GameLoopSimulator(
            ILogService logService,
            IHubService hubService,
            IGameStateService gameStateService,
            IPlayerActionCollection playerActionQueue,
            IPlayerActionExecutor playerActionExecutor,
            IBattleSimulator battleSimulator
            )
        {
            this.logService = logService;
            this.hubService = hubService;
            this.gameStateService = gameStateService;
            this.playerActionExecutor = playerActionExecutor;
            this.battleSimulator = battleSimulator;
            this.playerActionQueue = playerActionQueue;
            syncCoordinator = new SyncCoordinator();
            playerActionExecutor.SetSyncCoordinator(syncCoordinator);
            tickCount = 0;
        }

        public void StartSimulation()
        {
            logService.Log("Starting Game Simulation");

            while (true) {

                // Start
                BeforeStart();

                // Fluff
                PingAllClients();

                // Do player and section simulations
                SimulateUniverse();
                SendSyncMessages();

                // End
                EndOfTheSimulation();
            }
        }

        private void SendSyncMessages()
        {
            syncCoordinator.AreasToSync = syncCoordinator.AreasToSync.Distinct().ToList();
            foreach (var area in syncCoordinator.AreasToSync) {
                var areaSyncData = gameStateService.GetAreaSyncData(area);
                hubService.BroadcastAreaSync(areaSyncData);
            }
            
        }

        private void SimulateUniverse()
        {
            // Copy Action Collection (First thing)
            playerActionExecutor.SetActionCollection(playerActionQueue.CopyActionCollection());
            playerActionExecutor.ExecuteMovementActions();
            playerActionExecutor.ExecuteStartBattleActions();
            battleSimulator.SimulateBattles();
        }

        private Stopwatch BeforeStart()
        {
            stopwatch = Stopwatch.StartNew();
            return stopwatch;
        }

        private void EndOfTheSimulation()
        {
            stopwatch.Stop();
            tickCount++;
            Thread.Sleep(1000);
        }

        private void PingAllClients()
        {
            hubService.BroadcastPing();
        }
    }
}
