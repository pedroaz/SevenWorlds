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

    public class MonsterData
    {
        public string Id;
        public MonsterType MonsterType;
        public CombatData CombatData { get; set; }

        public MonsterData(MonsterDescription description, List<SkillData> skills, int level)
        {
            Id = Guid.NewGuid().ToString();
            CombatData = new CombatData(Id, skills);
            CombatData.MaxHp = description.MaxHp;
        }
    }
}
