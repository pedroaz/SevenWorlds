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
                MonsterData target = battleData.MonsterDatas.Find(x => x.Id == characterCombatData.TargetId);

                if(target != null) {
                    ApplyDamageToMonster(characterCombatData, target);
                }
            }

            // Monsters Attacks!
            foreach (MonsterData monsterData in battleData.MonsterDatas) {

                CharacterData target = battleData.CharacterDatas.Find(x => x.Id == monsterData.CombatData.TargetId);
            }
        }

        private void ApplyDamageToCharacter(CharacterData characterData, CombatData characterCombatData, MonsterData monsterData)
        {
            var attack = monsterData.CombatData.GetCurrentSkillDamage();
            var defense = characterCombatData.Defense;

            // Calculate difference between elements
            characterData.HpData.CurrentHp -= attack - defense;

        }

        private void ApplyDamageToMonster(CombatData characterCombatData, MonsterData monsterData)
        {
            var attack = characterCombatData.GetCurrentSkillDamage();
            var defense = monsterData.CombatData.Defense;

            monsterData.HpData.CurrentHp -= attack - defense;
        }
    }
}
