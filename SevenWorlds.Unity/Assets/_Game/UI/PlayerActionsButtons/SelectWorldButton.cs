using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectWorldButton : GameButton
{
    [HideInInspector]
    public int WorldIndex;

    private TextMeshProUGUI btnText;

    public override void Setup()
    {
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public override async Task OnClick()
    {
        GameState.SetCurrentWorld(WorldIndex);
        UIEvents.ChangeGameText(GameTextId.WorldName, GameState.WorldName);

        // Get characters from server
        var characters = await NetworkService.Object.RequestPlayerCharacters(GameState.PlayerName);

        if(characters == null) {
            LOG.Log($"Player does not have any characters: {GameState.PlayerName}");
            return;
        }

        var character = characters.Find(x => x.WorldId == GameState.WorldId);

        if(character == null) {
            LOG.Log($"Player does not have a character on world: {GameState.WorldId}");
            await ScreenChangerService.Object.ChangeScreen(ScreenId.CreateCharacter);
        }
        else {
            LOG.Log($"Found Character!");
            await ScreenChangerService.Object.ChangeScreen(ScreenId.SelectCharacter);
        }
    }

    public void Refresh(WorldData data)
    {
        btnText.text = data.Name;
    }
}
