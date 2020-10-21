using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Encounters
{
    public enum BattleStatus
    {
        BeforeStart,
        InProgress,
        Finished
    }

    public class BattleData : NetworkData
    {
        public List<string> CharaterIds { get; set; }
        public MonsterType MonsterType { get; set; }
        public MonsterData Monster{ get; set; }
        public int MaxAmountOfCharacters { get; set; }
        public BattleStatus Status { get; set; }
        public BattleData(StartBattleActionData startData)
        {
            Status = BattleStatus.BeforeStart;
        }

        public void SetMonsterData(MonsterData data)
        {
            Monster = data;
        }
    }
}
