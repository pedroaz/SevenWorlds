using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenChangerService : GameService<ScreenChangerService>
{
    private List<GameScreen> screens;

    [HideInInspector]
    public GameScreen currentScreen;

    private void Awake()
    {
        Object = this;
        screens = FindObjectsOfType<GameScreen>().ToList();
        currentScreen = screens.Find(x => x.Id == ScreenId.Black);
        foreach (var screen in screens) {
            screen.Hide();
        }
    }


    public void ChangeScreen(ScreenId id)
    {
        switch (id) {
            case ScreenId.Black:
                break;
            case ScreenId.Chat:
                break;
            case ScreenId.Login:
                break;
            case ScreenId.Universe:
                UniverseScreenRefresherService.Object.Refresh();
                break;
            case ScreenId.World:
                break;
            case ScreenId.Area:
                break;
        }

        if (currentScreen.Id != id) {
            currentScreen.Hide();
            var screen = screens.Find(x => x.Id == id);
            screen.Show();
            currentScreen = screen;
        }
    }
}
