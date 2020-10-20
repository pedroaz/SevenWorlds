using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Encounters
{
    public class BattleEncounterData : EncounterData
    {
        public List<string> CharaterIds { get; set; }
        public string MonsterType { get; set; }
        public int MaxAmountOfCharacters { get; set; }
        public bool HasBattleEnded { get; set; }

        public BattleEncounterData(StartBattleActionData startData)
        {
            Type = EncounterType.Battle.ToString();
        }
    }
}
