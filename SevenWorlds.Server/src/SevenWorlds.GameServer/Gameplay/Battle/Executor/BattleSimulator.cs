using MongoDB.Bson;
using SevenWorlds.GameServer.Gameplay.Battle.AI;
using SevenWorlds.GameServer.Gameplay.Battle.Executor;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
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
        private readonly ILogService logService;
        private readonly ISkillSimulator skillSimulator;

        public BattleSimulator(IGameStateService gameStateService, IMonsterAIService monsterAIService, 
            ILogService logService, ISkillSimulator skillSimulator)
        {
            this.gameStateService = gameStateService;
            this.monsterAIService = monsterAIService;
            this.logService = logService;
            this.skillSimulator = skillSimulator;
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

            foreach (var character in battleData.Characters) {
                var targets = FindTargets(battleData, character.CombatData.TargetIds);
                skillSimulator.SimulateSkill(character.CombatData, targets, character.Resources);
            }
            foreach (var monster in battleData.Monsters) {
                var targets = FindTargets(battleData, monster.CombatData.TargetIds);
                skillSimulator.SimulateSkill(monster.CombatData, targets, null);
            }
        }

        private List<CombatData> FindTargets(BattleData battleData, List<string> ids)
        {
            List<CombatData> targets = new List<CombatData>();

            foreach (var item in battleData.Characters) {
                if (ids.Contains(item.CombatData.UnitId)) targets.Add(item.CombatData);
            }
            foreach (var item in battleData.Monsters) {
                if (ids.Contains(item.CombatData.UnitId)) targets.Add(item.CombatData);
            }
            return targets;
        }
        
    }
}
