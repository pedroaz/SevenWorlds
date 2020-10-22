using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
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
        public List<CharacterData> CharacterDatas { get; set; }
        public List<CombatData> CharactersCombatData { get; set; }
        public List<MonsterData> MonsterDatas { get; set; }
        public int MaxAmountOfCharacters { get; set; }
        public BattleStatus Status { get; set; }

        public BattleData(StartBattleActionData startData)
        {
            Status = BattleStatus.BeforeStart;
            MaxAmountOfCharacters = startData.MaxAmountOfCharacters;
        }
    }
}
