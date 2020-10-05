using Microsoft.AspNet.SignalR;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Simulation
{
    public class GameLoopSimulator : IGameLoopSimulator
    {
        private ISectionCollection sectionCollection { get; }
        private ILogService logService { get; }
        private IHubContext hubContext { get; }

        public GameLoopSimulator(ISectionCollection sectionCollection, ILogService logService)
        {
            hubContext = GlobalHost.ConnectionManager.GetHubContext<MainHub>();

            this.sectionCollection = sectionCollection;
            this.logService = logService;
        }

        public void StartSimulation()
        {
            while (true) {
                logService.Log("Server Simulation Tick");
                PingAllClients();
                Thread.Sleep(1000);
            }
        }

        private void PingAllClients()
        {
            var clients = hubContext.Clients.All.OnPing(new PingData());
        }
    }
}
