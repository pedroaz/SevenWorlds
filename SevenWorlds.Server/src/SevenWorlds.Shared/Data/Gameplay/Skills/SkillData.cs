using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Skills
{
    public enum SkillType
    {
        BaseAttack,
        DoubleAttack,
        TrippleAttack
    }

    public class SkillData
    {
        public string SkillType { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public Dictionary<CharacterResourceType, int> ResourcesCost;

        public bool IsOfType(SkillType type)
        {
            return type.ToString().Equals(SkillType);
        }

    }
}
