using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class CombatData
    {
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
            SelectedSkill = SkillType.BaseAttack;
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

            var result = (int) new DataTable().Compute(sb.ToString(), "");

            return result;
        }

        public CombatData Copy()
        {
            CombatData data = new CombatData(this.UnitId, Skills);

            data.UnitId = this.UnitId;
            data.TargetId = this.TargetId;

            data.SelectedSkill = this.SelectedSkill;
            data.Attack = this.Attack;
            data.Defense = this.Defense;
            data.Speed = this.Speed;
            data.IsAlive = this.IsAlive;

            data.Fire = this.Fire;
            data.Water = this.Water;
            data.Earth = this.Earth;
            data.Air = this.Air;

            return data;
        }
    }
}
