using SevenWorlds.GameServer.Gameplay.Actions.Executor;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
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
        private readonly IPlayerActionCollection playerActionQueue;
        private Stopwatch stopwatch;
        private SyncCoordinator syncCoordinator;

        private int tickCount;

        public GameLoopSimulator(
            ILogService logService,
            IHubService hubService,
            IGameStateService gameStateService,
            IPlayerActionCollection playerActionQueue,
            IPlayerActionExecutor playerActionExecutor
            )
        {
            this.logService = logService;
            this.hubService = hubService;
            this.gameStateService = gameStateService;
            this.playerActionExecutor = playerActionExecutor;
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

                PingTickCount();

                // Fluff
                PingAllClients();
                //PrintWhoIsLogged();

                // Do player and section simulations
                SimulateUniverse();

                // End
                EndOfTheSimulation();
            }
        }

        private void SimulateUniverse()
        {
            // Copy Action Collection (First thing)
            playerActionExecutor.SetActionCollection(playerActionQueue.CopyActionCollection());

            playerActionExecutor.ExecuteMovementActions();
        }

        private void PingTickCount()
        {
            if (tickCount % 60 == 0) {
                logService.Log($"Tick count: {tickCount}");
            }
        }

        

        private Stopwatch BeforeStart()
        {
            logService.Log("----- Start of Server Simulation Tick -----", LogDestination.File);
            stopwatch = Stopwatch.StartNew();
            return stopwatch;
        }

        private void EndOfTheSimulation()
        {
            stopwatch.Stop();
            LogInsideTick($"Loop took {stopwatch.ElapsedMilliseconds} miliseconds");
            logService.Log("----- End of Server Simulation Tick -----", LogDestination.File);
            tickCount++;
            Thread.Sleep(1000);
        }

        private void LogInsideTick(string message)
        {
            logService.Log($"    {message}", LogDestination.File);
        }

        private void PrintWhoIsLogged()
        {
            var players = gameStateService.PlayerCollection.GetAll();
            if (players.Any()) {
                foreach (PlayerData data in players) {
                    LogInsideTick($"{data.PlayerName} is in the game");
                }
            }
            else {
                LogInsideTick($"No player is in the game");
            }

        }

        private void PingAllClients()
        {
            hubService.BroadcastPing();
        }
    }
}
