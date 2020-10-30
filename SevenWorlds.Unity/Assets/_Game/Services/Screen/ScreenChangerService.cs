using SevenWorlds.Shared.UnityLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenChangerService : GameService<ScreenChangerService>
{
    private List<GameScreen> screens;

    public GameScreen currentScreen;

    private void Awake()
    {
        Object = this;
        screens = FindObjectsOfType<GameScreen>().ToList();
        currentScreen = screens.Find(x => x.Id == ScreenId.Loading);
    }

    public static void HideAll()
    {
        foreach (var screen in Object.screens) {
            screen.Hide();
        }
    }

    public static async Task ChangeScreen(ScreenId id)
    {
        LOG.Log($"Chaning screen to: {id}");

        switch (id) {
            case ScreenId.Universe:
                await UniverseScreenRefresherService.Object.Refresh();
                break;
            case ScreenId.Area:
                await AreaScreenRefresherService.Object.Refresh();
                break;
            case ScreenId.CreateCharacter:
                CharacterCreationScreenRefresherService.Object.Refresh();
                break;
        }

        if (Object.currentScreen.Id != id) {
            Object.currentScreen.Hide();
            var screen = Object.screens.Find(x => x.Id == id);
            screen.Show();
            Object.currentScreen = screen;
        }
    }
}
