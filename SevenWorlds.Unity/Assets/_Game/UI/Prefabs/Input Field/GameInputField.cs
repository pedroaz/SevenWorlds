using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInputField : MonoBehaviour
{
    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    public string GetValue()
    {
        return inputField.text;
    }
}
