using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CreateCharacterButton : GameButton
{
    public override async Task OnClick()
    {

        try {
            if (!CharacterCreationScreenRefresherService.GetHasCharacterTypeSelected()) return;

            var characterType = CharacterCreationScreenRefresherService.GetSelectedCharacterType();

            LOG.Log($"Sending a request to create character: {GameState.PlayerName} on {GameState.CurrentWorld.Id} and type {characterType}");
            var result = await NetworkService.RequestCreateCharacter(
                GameState.PlayerName,
                GameState.CurrentWorld.Id,
                characterType
            );
            LOG.Log($"Character creation result was: {result}");
        }
        catch (System.Exception e) {
            LOG.Log(e);
            throw;
        }

       
    }
}
