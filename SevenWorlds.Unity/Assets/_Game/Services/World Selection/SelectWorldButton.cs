using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectWorldButton : GameButton
{
    [HideInInspector]
    public int WorldIndex;

    public GameText worldNameText;
    public GameText hasCharacterText;

    public override async Task OnClick()
    {
        GameState.SetCurrentWorld(WorldIndex);
        UIEvents.ChangeGameText(GameTextId.WorldName, GameState.WorldName);

        // Get characters from server
        var character = GameState.Object.Characters.Find(x => x.WorldId == GameState.WorldId);

        if(character == null) {
            LOG.Log($"Player does not have a character on world: {GameState.WorldId}");
            await ScreenChangerService.Object.ChangeScreen(ScreenId.CreateCharacter);
        }
        else {
            LOG.Log($"Found Character!");
            await ScreenChangerService.Object.ChangeScreen(ScreenId.SelectCharacter);
        }
    }

    public void Refresh(WorldData data, CharacterData characterData)
    {
        worldNameText.SetText(data.Name);
        if(characterData == null) {
            hasCharacterText.SetText("No Character");
        }
        else {
            hasCharacterText.SetText($"Character Lvl: {characterData.Level}");
        }
    }
}
