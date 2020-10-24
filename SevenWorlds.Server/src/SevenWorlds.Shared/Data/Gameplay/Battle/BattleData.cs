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
        public List<MonsterData> Monsters;
        public List<CharacterData> Characters;
        public int MaxAmountOfCharacters;
        public BattleStatus Status;

        public BattleData(StartBattleActionData startData)
        {
            Status = BattleStatus.BeforeStart;
            MaxAmountOfCharacters = startData.MaxAmountOfCharacters;
        }
    }
}
