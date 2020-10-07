using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public GameTextId Id;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string t)
    {
        textMesh.text = t;
    }

    public void AppendTextToNewLine(string t)
    {
        textMesh.text += $"\n{t}";
    }
}

public enum GameTextId
{
    None,
    IsConnectedToServer
}
