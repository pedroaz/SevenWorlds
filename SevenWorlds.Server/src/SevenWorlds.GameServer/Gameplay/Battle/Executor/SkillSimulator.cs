using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Executor
{
    public class SkillSimulator : ISkillSimulator
    {
        private readonly ILogService logService;

        public SkillSimulator(ILogService logService)
        {
            this.logService = logService;
        }

        public void SimulateSkill(CombatData caster, List<CombatData> targets, WorldResourcesData resourcesData)
        {
            if (!CanSimulateSkill(caster, targets)) {
                
                return;
            }

            SkillData skillData = caster.SelectedSkill;
            SkillType skillType = skillData.Type;
            
            logService.Log($"Casting skill type: {skillType}", type: LogType.Battle);

            // Resources cost if is a character
            if(resourcesData != null) {
                if (!resourcesData.HasEnoughForSkill(skillData.ResourcesCost)) {
                    logService.Log($"Not enough resources to cast", type: LogType.Battle);
                    return;
                }
                resourcesData.ApplySkillCost(skillData.ResourcesCost);
            }


            // Apply the skill
            switch (skillType) {
                case SkillType.WeaponAttack:
                    WeaponAttack(caster, targets);
                    break;
                case SkillType.GoldStrike:
                    GoldStrike(caster, targets);
                    break;
                default:
                    logService.Log($"No Skill Type. How did that happen?", type: LogType.Battle);
                    break;
            }
        }

       

        private bool CanSimulateSkill(CombatData caster, List<CombatData> targets)
        {
            if(caster == null) {
                logService.Log($"Caster is null. Wil not simulate", type: LogType.Battle);
                return false;
            }

            if (targets == null) {
                logService.Log($"Targets is null. Wil not simulate", type: LogType.Battle);
                return false;
            }

            if (caster.SelectedSkill == null) {
                logService.Log($"Caster selected skill is null. Wil not simulate", type: LogType.Battle);
                return false;
            }

            if (targets.Count < 1) {
                logService.Log($"No target. Wil not simulate", type: LogType.Battle);
                return false;
            }

            return true;
        }

        private void WeaponAttack(CombatData caster, List<CombatData> targets)
        {
            foreach (var target in targets) {
                target.CurrentHp -= caster.Attack;
            }
        }

        private void GoldStrike(CombatData caster, List<CombatData> targets)
        {
            foreach (var target in targets) {
                target.CurrentHp -= (caster.Attack + 10);
            }
        }
    }
}
