using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.ActionDatas
{
    public class StartBattleActionData : PlayerActionData
    {
        public List<string> InitialCharacters;
        public List<MonsterType> Monsters;
        public int MaxAmountOfCharacters;

        public StartBattleActionData(List<MonsterType> list, List<string> initialCharacters, int maxAmountOfCharacters)
        {
            InitialCharacters = initialCharacters;
            Monsters = list;
            MaxAmountOfCharacters = maxAmountOfCharacters;
        }
    }
}
