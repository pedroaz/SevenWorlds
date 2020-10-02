using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenChangerService : MonoBehaviour
{
    [HideInInspector]
    public static ScreenChangerService Object;
    private List<GameScreen> screens;

    [HideInInspector]
    public GameScreen currentScreen;

    private void Awake()
    {
        Object = this;
        screens = FindObjectsOfType<GameScreen>().ToList();
        currentScreen = screens.Find(x => x.Id == ScreenId.Black);
    }


    public void ChangeScreen(ScreenId id)
    {
        if (currentScreen.Id != id) {
            currentScreen.Hide();
            screens.Find(x => x.Id == id).Show();
        }
    }
}
