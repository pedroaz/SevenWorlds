﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    public class MonsterCampData : SectionData
    {
        public MonsterType monsterType;
        public MonsterCampData(MonsterType monsterType)
        {
            this.monsterType = monsterType;
            Type = SectionType.MonsterCamp.ToString();
        }
    }
}
