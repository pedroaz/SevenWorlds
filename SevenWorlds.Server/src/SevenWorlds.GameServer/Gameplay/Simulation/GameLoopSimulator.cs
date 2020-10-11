using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
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
        private readonly IPlayerActionQueue playerActionQueue;
        private readonly IPlayerActionFactory playerActionFactory;
        private Stopwatch stopwatch;

        public GameLoopSimulator(
            ILogService logService,
            IHubService hubService,
            IGameStateService gameStateService,
            IPlayerActionQueue playerActionQueue,
            IPlayerActionFactory playerActionFactory
            )
        {
            this.logService = logService;
            this.hubService = hubService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
            this.playerActionFactory = playerActionFactory;
        }

        public void StartSimulation()
        {
            while (true) {

                // Start
                BeforeStart();

                // Fluff
                PingAllClients();
                PrintWhoIsLogged();

                // Player Actions
                SimulatePlayerActions();

                // End
                EndOfTheSimulation();
            }
        }

        private void SimulatePlayerActions()
        {
            foreach (var playerActionData in playerActionQueue.GetAllFromQueue()) {
                playerActionFactory.GenerateAction(playerActionData).Execute();
            }
        }

        private Stopwatch BeforeStart()
        {
            logService.Log("----- Start of Server Simulation Tick -----");
            stopwatch = Stopwatch.StartNew();
            return stopwatch;
        }

        private void EndOfTheSimulation()
        {
            stopwatch.Stop();
            LogInsideTick($"Loop took {stopwatch.ElapsedMilliseconds} miliseconds");
            logService.Log("----- End of Server Simulation Tick -----");
            Thread.Sleep(1000);
        }

        private void LogInsideTick(string message)
        {
            logService.Log($"    {message}");
        }

        private void PrintWhoIsLogged()
        {
            var players = gameStateService.PlayerCollection.GetAll();
            if (players.Any()) {
                foreach (PlayerData data in players) {
                    LogInsideTick($"{data.Name} is in the game");
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
