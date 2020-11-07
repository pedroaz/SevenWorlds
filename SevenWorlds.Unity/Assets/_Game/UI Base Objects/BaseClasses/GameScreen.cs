using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenId
{
    Loading,
    Chat,
    Login,
    Universe,
    Area,
    Register,
    SelectCharacter,
    CreateCharacter,
    SpiritShop,
    Temp,
    SpiritActionHouse,
    Raids,
    PlayerInfo
}

public class GameScreen : MonoBehaviour
{
    public ScreenId Id;
    private GameObject contianer;

    private void Awake()
    {
        contianer = transform.GetChild(0).gameObject;
    }

    public void Show()
    {
        contianer.SetActive(true);
    }

    public void Hide()
    {
        if(contianer == null) {
            LOG.Log($"{Id.ToString()} screen does not have a container!");
        }
        contianer.SetActive(false);
    }
}
