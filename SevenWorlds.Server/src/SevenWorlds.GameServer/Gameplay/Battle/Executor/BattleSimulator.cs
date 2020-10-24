using MongoDB.Bson;
using SevenWorlds.GameServer.Gameplay.Battle.AI;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.Shared.Data.Factories;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Encounter.Executor
{
    public class BattleSimulator : IBattleSimulator
    {
        private readonly IGameStateService gameStateService;
        private readonly IMonsterAIService monsterAIService;

        public BattleSimulator(IGameStateService gameStateService, IMonsterAIService monsterAIService)
        {
            this.gameStateService = gameStateService;
            this.monsterAIService = monsterAIService;
        }

        public void SimulateBattles()
        {
            foreach (BattleData battleData in gameStateService.BattleCollection.GetAll()) {
                switch (battleData.Status) {
                    case BattleStatus.BeforeStart:
                        break;
                    case BattleStatus.InProgress:
                        Simulate(battleData);
                        break;
                    case BattleStatus.Finished:
                        break;
                }
            }
        }

        private void Simulate(BattleData battleData)
        {
            // Set target and skill of monsters 
            foreach (var monster in battleData.Monsters) {
                monsterAIService.Simulate(monster);
            }
        }

        
    }
}
