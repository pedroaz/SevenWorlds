using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Monster;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum MonsterType
    {
        Poring,
        PecoPeco
    }

    public class MonsterData : NetworkData
    {
        public MonsterType MonsterType;
        public CombatData CombatData { get; set; }


        public MonsterData(MonsterSeed seed, List<SkillData> skills, int level)
        {
            CombatData = new CombatData(Guid.NewGuid().ToString(), skills);
            CombatData.MaxHp = seed.MaxHp;
        }
    }
}
