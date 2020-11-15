using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlegroundService : GameService<BattlegroundService>
{
    public Transform unitContainer;
    public List<Unit> Units;
    private AreaSyncData previousData;
    public GameObject unitPrefab;

    private void Awake()
    {
        Object = this;
    }

    public static void SyncArea(AreaSyncData data)
    {
        UIEvents.ChangeGameText(GameTextId.AreaName, data.Area.Name);

        SyncBattleground(data);
    }

    private static void SyncBattleground(AreaSyncData data)
    {
        AddRemoveUnits(data);
        SyncCharacters(data);
        SyncMonsters(data);
    }

    private static void AddRemoveUnits(AreaSyncData data)
    {
        List<string> wasRemoved, wasAdded;
        GetIds(data, out wasRemoved, out wasAdded);
        RemoveUnits(data, wasRemoved);
        AddUnits(data, wasAdded);
    }

    private static void GetIds(AreaSyncData data, out List<string> wasRemoved, out List<string> wasAdded)
    {
        List<string> previousIds = Object.previousData.Characters.Select(x => x.Id).ToList();
        previousIds.AddRange(Object.previousData.Monsters.Select(x => x.Id).ToList());
        List<string> currentIds = data.Characters.Select(x => x.Id).ToList();
        currentIds.AddRange(data.Monsters.Select(x => x.Id));

        wasRemoved = previousIds.Except(currentIds).ToList();
        wasAdded = currentIds.Except(previousIds).ToList();
    }

    private static void RemoveUnits(AreaSyncData data, List<string> ids)
    {

        foreach (var id in ids) {
            var unit = Object.Units.Find(x => x.combatData.UnitId == id);
            if (unit != null) {
                Destroy(unit.gameObject);
            }
        }
    }

    private static void AddUnits(AreaSyncData data, List<string> ids)
    {
        foreach (var id in ids) {
            
            GameObject go = Instantiate(Object.unitPrefab, Object.unitContainer, false);
            
            var character = data.Characters.Find(x => x.CombatData.UnitId == id).CombatData;
            var monster = data.Monsters.Find(x => x.CombatData.UnitId == id).CombatData;

            if (character != null) {
                go.GetComponent<Unit>().SyncCombatData(character);
            }
            else if(monster != null) {
                go.GetComponent<Unit>().SyncCombatData(monster);
            }
        }
    }

    private static void SyncCharacters(AreaSyncData data)
    {
        foreach (var charater in data.Characters) {
            var unit = Object.Units.Find(x => x.combatData.UnitId == charater.CombatData.UnitId);
            if (unit != null) {
                unit.SyncCombatData(charater.CombatData);
            }
        }
    }


    private static void SyncMonsters(AreaSyncData data)
    {
        foreach (var monster in data.Monsters) {
            var unit = Object.Units.Find(x => x.combatData.UnitId == monster.CombatData.UnitId);
            if (unit != null) {
                unit.SyncCombatData(monster.CombatData);
            }
        }
    }

    
    
}
