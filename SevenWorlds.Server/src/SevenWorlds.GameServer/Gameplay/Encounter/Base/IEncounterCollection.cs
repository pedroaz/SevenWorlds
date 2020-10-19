using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Encounter
{
    public interface IEncounterCollection
    {
        EncounterBundle Bundle { get; }
        List<BattleEncounterData> Battles { get; }
        BattleEncounterData FindBattleEncounter(string id);
        void NewBattle(StartBattleActionData data);
    }
}
