using SevenWorlds.GameServer.Gameplay.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Encounter.Executor
{
    public class EncounterExecutor : IEncounterExecutor
    {
        private readonly IGameStateService gameStateService;

        public EncounterExecutor(IGameStateService gameStateService)
        {
            this.gameStateService = gameStateService;
        }

        public void ExecuteBattleEncounters()
        {
            foreach (var item in gameStateService.EncounterCollection.Battles) {

            }
        }
    }
}
