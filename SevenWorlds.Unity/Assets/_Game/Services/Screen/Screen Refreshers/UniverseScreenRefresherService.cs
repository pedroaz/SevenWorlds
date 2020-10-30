﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UniverseScreenRefresherService : GameService<UniverseScreenRefresherService>
{
    List<SelectWorldButton> buttons;

    private void Awake()
    {
        Object = this;
        buttons = Resources.FindObjectsOfTypeAll<SelectWorldButton>().ToList();
    }

    public async Task Refresh()
    {
        var universeData = await NetworkService.RequestUniverseSyncData();
        var characters = await NetworkService.RequestPlayerCharacters(GameState.PlayerName);

        var worlds = GameState.Worlds;

        var interactive = GameState.PlayerData.AvailableCharacters.Any();

        for (int i = 0; i < 7; i++) {
            var btn = buttons.Find(x => x.WorldIndex == i);
            var worldData = worlds?.Find(x => x.WorldIndex == i);
            var characterData = characters.Find(x => x.WorldId == worldData.Id);
            btn?.Refresh(worldData, characterData);
            btn.SetInteractable(interactive);
        }
    }
}
