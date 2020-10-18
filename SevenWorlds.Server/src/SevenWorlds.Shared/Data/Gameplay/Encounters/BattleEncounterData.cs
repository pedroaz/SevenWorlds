using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Encounters
{
    public class BattleEncounterData : EncounterData
    {
        public List<string> CharaterIds { get; set; }
        public MonsterData Monster { get; set; }
        public int MaxAmountOfCharacters { get; set; }
        public bool HasBattleEnded { get; set; }

        public BattleEncounterData(StartBattleActionData startData)
        {
            Type = EncounterType.Battle;
        }
    }
}
