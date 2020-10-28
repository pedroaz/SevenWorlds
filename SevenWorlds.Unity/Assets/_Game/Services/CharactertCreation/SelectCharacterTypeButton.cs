using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectCharacterTypeButton : GameButton
{
    private CharacterType type;

    public void Setup(CharacterType type)
    {
        this.type = type;
        GetComponentInChildren<GameText>().SetText(type.ToString());
    }

    public override Task OnClick()
    {
        CharacterCreationScreenRefresherService.Object.hasCharacterTypeSelected = true;
        CharacterCreationScreenRefresherService.Object.characterType = type;
        UIEvents.ChangeGameText(GameTextId.SelectedCharacterType, type.ToString());
        return base.OnClick();
    }
}
