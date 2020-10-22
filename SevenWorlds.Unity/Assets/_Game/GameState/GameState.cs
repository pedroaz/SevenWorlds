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
}
