using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : GameService<GameState>
{
    public PlayerData playerData;
    public UniverseData universe;
    public List<WorldData> worlds;
    public WorldData currentWorld;
    public List<AreaData> areas;
    public AreaData currentArea;
    public SectionBundle sections;
    public List<CharacterData> characters;
    public CharacterData currentCharacter;

    private void Awake()
    {
        Object = this;
    }

    public static PlayerData PlayerData { get => Object.playerData; set => Object.playerData = value; }
    public static string PlayerName { get => Object.playerData.PlayerName; }
    public static UniverseData Universe { get => Object.universe; set => Object.universe = value; }
    public static List<WorldData> Worlds { get => Object.worlds; set => Object.worlds = value; }
    public static WorldData CurrentWorld { get => Object.currentWorld; set => Object.currentWorld = value; }
    public static List<AreaData> Areas { get => Object.areas; set => Object.areas = value; }
    public static AreaData CurrentArea { get => Object.currentArea; set => Object.currentArea = value; }
    public static SectionBundle Sections { get => Object.sections; set => Object.sections = value; }
    public static List<CharacterData> Characters { get => Object.characters; set => Object.characters = value; }

    public static void SetCurrentWorldByWorldIndex(int worldIndex)
    {
        Object.currentWorld = Object.worlds.Find(x => x.WorldIndex == worldIndex);
    }

    public static CharacterData GetCharacterByWorldId(string worldId)
    {
        return Object.characters.Find(x => x.WorldId == worldId);
    }

    public static void RefreshPlayerData()
    {

    }
}
