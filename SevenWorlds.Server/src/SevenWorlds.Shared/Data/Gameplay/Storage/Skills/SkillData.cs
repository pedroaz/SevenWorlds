using SevenWorlds.Shared.Data.Gameplay.Storage.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Skills
{
    public enum SkillType
    {
        WeaponAttack,
        GoldStrike,
    }

    public enum SkillTargetType
    {
        Self,
        Single,
        AllEnemies,
        AllAllys,
        Random
    }

    public class SkillData
    {
        // Description
        public SkillType Type;
        public SkillTargetType TargetType;
        public string Description;
        public string DamageDescription;
        public int BaseCD;

        // Resources
        public Dictionary<WorldResourceType, int> ResourcesCost;

        public SkillData(SkillDescription description)
        {
            Type = description.Type;
            TargetType = description.TargetType;
            Description = description.Description;
            DamageDescription = description.DamageDescription;
            BaseCD = description.BaseCD;
        }
    }
}
