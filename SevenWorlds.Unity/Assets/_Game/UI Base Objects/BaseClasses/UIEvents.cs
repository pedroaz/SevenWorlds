using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public delegate void GameTextChangedEventHandler(object sender, GameTextChangedArgs e);
    public static event GameTextChangedEventHandler OnGameTextChanged = delegate { };
    public static void ChangeGameText(GameTextId id, string value)
    {
        OnGameTextChanged(null, new GameTextChangedArgs() {
            gameTextId = id,
            value = value
        });
    }
}

public class GameTextChangedArgs : EventArgs
{
    public GameTextId gameTextId;
    public string value;
}

