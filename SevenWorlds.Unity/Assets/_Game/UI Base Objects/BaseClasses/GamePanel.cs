using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePanelId
{
    EscMenu,
    Map
}

public class GamePanel : MonoBehaviour
{
    public GamePanelId Id;

    public bool IsOpen = false;

    [HideInInspector]
    public GameObject contianer;

    private void Awake()
    {
        contianer = transform.GetChild(0).gameObject;
    }

    public void Show()
    {
        contianer.SetActive(true);
        IsOpen = true;
    }

    public void Hide()
    {
        contianer.SetActive(false);
        IsOpen = false;
    }
}
