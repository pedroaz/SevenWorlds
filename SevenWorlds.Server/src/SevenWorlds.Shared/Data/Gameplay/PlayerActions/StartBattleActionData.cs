using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.ActionDatas
{
    public class StartBattleActionData : PlayerActionData
    {
        public List<string> InitialCharacters { get; set; }
        public List<MonsterType> Monsters { get; set; }
        public int MaxAmountOfCharacters { get; set; }

        public StartBattleActionData(List<MonsterType> list, List<string> initialCharacters, int maxAmountOfCharacters)
        {
            InitialCharacters = initialCharacters;
            Monsters = list;
            MaxAmountOfCharacters = maxAmountOfCharacters;
        }
    }
}
