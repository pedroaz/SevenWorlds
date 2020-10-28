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

    public void HideAll()
    {
        foreach (var screen in screens) {
            screen.Hide();
        }
    }

    public async Task ChangeScreen(ScreenId id)
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

        if (currentScreen.Id != id) {
            currentScreen.Hide();
            var screen = screens.Find(x => x.Id == id);
            screen.Show();
            currentScreen = screen;
        }
    }
}
