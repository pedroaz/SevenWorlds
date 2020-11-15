using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Storage.Skills
{
    public class SkillDescription
    {
        public SkillId SkillId;
        public SkillTargetType TargetType;
        public string Description;
        public string DamageDescription;
        public int BaseCD;
        public Dictionary<WorldResourceType, int> ResourcesCost;
    }
}
