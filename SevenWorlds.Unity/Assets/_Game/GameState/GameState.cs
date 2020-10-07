using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : GameService<GameState>
{
    public PlayerData PlayerData;
    public UniverseData Universe;
    public WorldData World;
    public AreaData Area;
    public List<SectionData> Sections;

    private void Awake()
    {
        Object = this;
    }
}
