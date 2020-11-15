using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Monster
{
    public class MonsterDescription
    {
        public MonsterType MonsterType;
        public int MaxHp;
        public List<SkillId> Skills;
    }
}
