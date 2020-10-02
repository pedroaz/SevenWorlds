using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
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
