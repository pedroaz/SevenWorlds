﻿using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public interface ISkillFactory
    {
        SkillData GetSkillData(SkillType type);
        List<SkillData> GetListOfSkillDatas (List<SkillType> types);
        void SetupStorage();
    }
}