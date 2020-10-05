using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Utils.Log;
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

        public GameLoopSimulator(ISectionCollection sectionCollection, ILogService logService)
        {
            this.sectionCollection = sectionCollection;
            this.logService = logService;
        }

        public void StartSimulation()
        {
            while (true) {
                logService.Log("Server Simulation Tick");
                Thread.Sleep(1000);
            }
        }
    }
}
