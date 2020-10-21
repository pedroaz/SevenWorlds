using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.ActionDatas
{
    public class StartBattleActionData : PlayerActionData
    {
        public List<MonsterType> Monsters { get; set; }

        //public StartBattleActionData(MonsterType monsterType)
        //{
        //    MonsterType = monsterType;
        //}
    }
}
