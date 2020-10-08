﻿using SevenWorlds.Shared.Data.Gameplay;
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
        UIEvents.ChangeGameText(GameTextId.PlayerName, GameState.Object.PlayerData.Name);
        UIEvents.ChangeGameText(GameTextId.UniverseName, GameState.Object.Universe.Name);
    }
}