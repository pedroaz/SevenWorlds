using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System.Collections.Generic;
using static SevenWorlds.Shared.Data.Gameplay.EncounterData;

namespace SevenWorlds.GameServer.Gameplay.Encounter
{
    public class EncounterCollection : IEncounterCollection
    {
        private EncounterBundle bundle = new EncounterBundle();
        public List<BattleEncounterData> Battles => bundle.Battles;

        public EncounterBundle Bundle => bundle;

        public BattleEncounterData FindBattleEncounter(string id)
        {
            return bundle.Battles.Find(x => x.Id == id);
        }

        public EncounterCollection()
        {
        }

        public void NewBattle(StartBattleActionData startData)
        {
            bundle.Battles.Add(new BattleEncounterData(startData));
        }
    }
}
