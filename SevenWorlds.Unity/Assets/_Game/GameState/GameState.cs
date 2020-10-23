using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : GameService<GameState>
{
    public PlayerData PlayerData;
    public UniverseData Universe;
    public List<WorldData> Worlds;
    public WorldData CurrentWorld;
    public List<AreaData> Areas;
    public AreaData CurrentArea;
    public SectionBundle Sections;

    private void Awake()
    {
        Object = this;
    }

    public static string PlayerName => Object?.PlayerData?.PlayerName;
    public static string WorldId => Object?.CurrentWorld?.Id;
    public static string WorldName => Object?.CurrentWorld?.Name;

    public static void SetCurrentWorld(int index)
    {
        Object.CurrentWorld = Object.Worlds.Find(x => x.WorldIndex == index);
    }
}
