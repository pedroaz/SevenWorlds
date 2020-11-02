using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Monster
{
    public class MonsterSeed
    {
        public MonsterType MonsterType;
        public int MaxHp;
        public List<SkillType> Skills;
    }
}
