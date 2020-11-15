using SevenWorlds.Shared.Data.Gameplay.Storage.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Skills
{
    public enum SkillId
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
        public SkillId SkillId;
        public SkillTargetType TargetType;
        public string Description;
        public string DamageDescription;
        public int BaseCD;

        // Resources
        public Dictionary<WorldResourceType, int> ResourcesCost;

        public SkillData(SkillDescription description)
        {
            SkillId = description.SkillId;
            TargetType = description.TargetType;
            Description = description.Description;
            DamageDescription = description.DamageDescription;
            BaseCD = description.BaseCD;
        }
    }
}
