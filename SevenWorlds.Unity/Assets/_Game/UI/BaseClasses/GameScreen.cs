using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenId
{
    Black,
    Chat,
    Login,
    Universe,
    World,
    Area,
    Register,
    SelectCharacter,
    CreateCharacter
}

public class GameScreen : MonoBehaviour
{
    public ScreenId Id;
    [HideInInspector]
    public GameObject contianer;

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
        contianer.SetActive(false);
    }
}
