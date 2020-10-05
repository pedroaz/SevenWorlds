using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Simulation
{
    public class GameLoopSimulator : IGameLoopSimulator
    {

        private ILogService logService { get; }
        private readonly IHubService hubService;
        private readonly IGameStateService gameStateService;

        public GameLoopSimulator(
            ILogService logService, 
            IHubService hubService,
            IGameStateService gameStateService
            )
        {
            this.logService = logService;
            this.hubService = hubService;
            this.gameStateService = gameStateService;
        }

        public void StartSimulation()
        {
            while (true) {
                logService.Log("----- Start of Server Simulation Tick -----");
                Stopwatch stopwatch = Stopwatch.StartNew();
                PingAllClients();
                ShowLoggedClients();
                stopwatch.Stop();
                LogInsideTick($"Loop took {stopwatch.ElapsedMilliseconds} miliseconds");
                logService.Log("----- End of Server Simulation Tick -----");
                Thread.Sleep(1000);
            }
        }

        private void LogInsideTick(string message)
        {
            logService.Log($"    {message}");
        }

        private void ShowLoggedClients()
        {
            var players = gameStateService.PlayerCollection.GetAll();
            if(players.Any()) {
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
            hubService.PingAll();
        }
    }
}
