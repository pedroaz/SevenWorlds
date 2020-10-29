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

        if (!CharacterCreationScreenRefresherService.Object.hasCharacterTypeSelected) return;

        var characterType = CharacterCreationScreenRefresherService.Object.characterType;

        LOG.Log($"Sending a request to create character: {GameState.PlayerName} on {GameState.Object.CurrentWorld.Id} and type {characterType}");
        var result = await NetworkService.Object.RequestCreateCharacter(
            GameState.PlayerName, 
            GameState.Object.CurrentWorld.Id,
            characterType
        );
        LOG.Log($"Character creation was a: {result}");
    }
}
