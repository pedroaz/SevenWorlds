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

                var targetId = monsterData.CombatData.TargetId;
                CharacterData target = battleData.CharacterDatas.Find(x => x.Id == targetId);
                CombatData targetCombatData = battleData.CharactersCombatData.Find(x => x.UnitId == targetId);
                ApplyDamageToCharacter(target, targetCombatData, monsterData);
            }
        }

        private void ApplyDamageToCharacter(CharacterData characterData, CombatData characterCombatData, MonsterData monsterData)
        {
            var damage = monsterData.CombatData.GetCurrentSkillDamage(characterCombatData);
            characterData.HpData.CurrentHp -= damage;
        }

        private void ApplyDamageToMonster(CombatData characterCombatData, MonsterData monsterData)
        {
            var damage = characterCombatData.GetCurrentSkillDamage(monsterData.CombatData);
            monsterData.HpData.CurrentHp -= damage;
        }
    }
}
