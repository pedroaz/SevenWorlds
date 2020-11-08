using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Encounters
{
    public class BattleData
    {
        public string Id;
        public List<MonsterData> Monsters;
        public List<CharacterData> Characters;
        public int MaxAmountOfCharacters;

        public BattleData()
        {
        }
    }
}
