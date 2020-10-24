using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class CombatData : NetworkData
    {
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

        public string UnitId { get; set; }
        public string TargetId { get; set; }

        public SkillType SelectedSkill { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public bool IsAlive { get; set; }

        public int Fire { get; set; }
        public int Water { get; set; }
        public int Earth { get; set; }
        public int Air { get; set; }

        // List of skills
        public List<SkillData> Skills { get; set; }

        public CombatData(string unitId, List<SkillData> skills)
        {
            UnitId = unitId;
            Skills = skills;
            SelectedSkill = SkillType.WeaponAttack;
            Attack = 1;
            IsAlive = true;
        }

        public int GetCurrentSkillDamage(CombatData targetCombatData)
        {
            SkillData skill = Skills.Find(x => x.IsOfType(SelectedSkill));

            StringBuilder sb = new StringBuilder(skill.Formula);

            sb.Replace("Attack", Attack.ToString());
            sb.Replace("Fire", Fire.ToString());
            sb.Replace("Water", Water.ToString());
            sb.Replace("Earth", Earth.ToString());
            sb.Replace("Air", Air.ToString());

            var result = (int)new DataTable().Compute(sb.ToString(), "");

            return result;
        }

        public void Fight(CombatData target)
        {
            var damage = GetCurrentSkillDamage(target);
            target.CurrentHp -= damage;
        }
    }
}
