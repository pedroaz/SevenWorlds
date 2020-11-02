using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameTextId
{
    None,
    IsConnectedToServer,
    PlayerName,
    UniverseName,
    WorldName,
    SelectedCharacterType,
    ZaiChat,
    SelectedQuestStatus,
    RegisterResponse
}

public class GameText : SetupMonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public GameTextId Id;

    public override void Setup()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        UIEvents.OnGameTextChanged += GameTextChanged;
    }

    public void Show(bool value)
    {
        textMesh.gameObject.SetActive(value);
    }

    private void GameTextChanged(object sender, GameTextChangedArgs e)
    {
        if(e.gameTextId == Id) {
            SetText(e.value);
        }
    }

    public void SetText(string t)
    {
        if (textMesh == null) {
            Setup();
        }

        if (textMesh == null) {
            LOG.Log($"Tring to set text to null object {name}", LogLevel.Warning);
            return;
        }

        

        textMesh.text = t;
    }

    public void AppendTextToNewLine(string t)
    {
        textMesh.text += $"\n{t}";
    }
}


