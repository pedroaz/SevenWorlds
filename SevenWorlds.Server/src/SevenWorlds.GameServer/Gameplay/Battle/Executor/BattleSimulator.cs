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

        public BattleSimulator(IGameStateService gameStateService)
        {
            this.gameStateService = gameStateService;
        }

        public void SimulateBattles()
        {
            foreach (BattleData battleData in gameStateService.BattleCollection.GetAll()) {
                switch (battleData.Status) {
                    case BattleStatus.BeforeStart:
                        
                        break;
                    case BattleStatus.InProgress:
                        Fight(battleData);
                        break;
                    case BattleStatus.Finished:
                        break;
                }
            }
        }

        private void Fight(BattleData battleData)
        {
            // Character Attacks!
            foreach (CombatData characterCombatData in battleData.CharactersCombatData) {
                MonsterData target = battleData.Monsters.Find(x => x.Id == characterCombatData.TargetId);

                if(target != null) {
                    ApplyDamageToMonster(characterCombatData, target);
                }
            }

            // Monsters Attacks!
        }

        private void ApplyDamageToCharacter(CharacterData characterData, CombatData characterCombatData, MonsterData monsterData)
        {

        }

        private void ApplyDamageToMonster(CombatData characterCombatData, MonsterData monsterData)
        {
            var attack = monsterData.CombatData.GetCurrentSkillDamage();
            var defense = characterCombatData.Defense;

            monsterData.HpData.CurrentHp -= attack - defense;
        }
    }
}
