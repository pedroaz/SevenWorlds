using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UniverseScreenRefresherService : GameService<UniverseScreenRefresherService>
{
    List<SelectWorldButton> buttons;

    public GameObject questButton;
    public GameObject playerInfoButton;
    public GameObject spiritShopButton;
    public GameObject spiritActionHouse;
    public GameObject raidsButton;

    private void Awake()
    {
        Object = this;
        buttons = Resources.FindObjectsOfTypeAll<SelectWorldButton>().ToList();

        if(questButton == null || playerInfoButton == null || spiritShopButton == null || raidsButton ==null || spiritActionHouse == null) {
            LOG.Log("One button is null!", LogLevel.Error);
        }
    }

    public static void Refresh()
    {
        SetZaiMessage();
        RefreshWorldButtons();
        RefreshBottomButtons();
    }

    private static void RefreshBottomButtons()
    {
        if (!GameState.HasAnyCharacterType) {
            Object.raidsButton.SetActive(false);
            Object.spiritShopButton.SetActive(false);
            Object.spiritActionHouse.SetActive(false);
            Object.playerInfoButton.SetActive(false);
        }
    }

    private static void RefreshWorldButtons()
    {
        for (int i = 0; i < 7; i++) {
            var btn = Object.buttons.Find(x => x.WorldIndex == i);
            var worldData = GameState.Worlds?.Find(x => x.WorldIndex == i);
            var characterData = GameState.PlayerCharacters.Find(x => x.WorldId == worldData.Id);
            btn?.Refresh(worldData, characterData);
            btn.SetInteractable(GameState.HasAnyCharacterType);
        }
    }

    private static void SetZaiMessage()
    {
        if (!GameState.HasAnyCharacterType) {
            UIEvents.ChangeGameText(GameTextId.ZaiChat, "You don't have a Spirit Warrior on your vessel... Come and talk to me.");
        }
    }
}
