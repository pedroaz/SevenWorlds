using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralRefresherService : GameService<GeneralRefresherService>
{
    private void Awake()
    {
        Object = this;
    }

    public void Refresh()
    {
        UIFinderService.Object.FindText(GameTextId.PlayerName).ForEach(x => x.SetText(GameState.Object.PlayerData.Name));
        UIFinderService.Object.FindText(GameTextId.UniverseName).ForEach(x => x.SetText(GameState.Object.Universe.Name));
    }
}
