using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenId
{
    Black,
    Chat
}

public class GameScreen : MonoBehaviour
{
    public ScreenId Id;
    public GameObject contianer;

    public void Show()
    {
        contianer.SetActive(true);
    }

    public void Hide()
    {
        contianer.SetActive(false);
    }
}
