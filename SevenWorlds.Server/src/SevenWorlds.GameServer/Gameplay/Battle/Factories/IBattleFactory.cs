﻿using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public interface IBattleFactory
    {
        BattleData CreateNewBattle(StartBattleActionData startData);
    }
}