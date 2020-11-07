using SevenWorlds.Shared.Data.Gameplay.Quests;
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
            case ScreenId.Login:
                LoginScreenRefresherService.Refresh();
                break;
            case ScreenId.Universe:
                UniverseScreenRefresherService.Refresh();
                break;
            case ScreenId.Area:
                await AreaScreenRefresherService.Refresh();
                break;
            case ScreenId.CreateCharacter:
                CharacterCreationScreenRefresherService.Refresh();
                break;
            case ScreenId.Temp:
                await QuestScreenRefresherService.Refresh(QuestStatus.Available);
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
