using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.Shared.Data.Factories;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Encounter.Executor
{
    public class BattleSimulator : IBattleSimulator
    {
        private readonly IGameStateService gameStateService;
        private readonly IMonsterDataFactory monsterDataFactory;

        public BattleSimulator(IGameStateService gameStateService, IMonsterDataFactory monsterDataFactory)
        {
            this.gameStateService = gameStateService;
            this.monsterDataFactory = monsterDataFactory;
        }

        public void SimulateBattles()
        {
            foreach (BattleData battleData in gameStateService.BattleCollection.GetAll()) {
                switch (battleData.Status) {
                    case BattleStatus.BeforeStart:
                        battleData.SetMonsterData(monsterDataFactory.GetMonsterData(battleData.MonsterType));
                        break;
                    case BattleStatus.InProgress:
                        break;
                    case BattleStatus.Finished:
                        break;
                }
            }
        }
    }
}
