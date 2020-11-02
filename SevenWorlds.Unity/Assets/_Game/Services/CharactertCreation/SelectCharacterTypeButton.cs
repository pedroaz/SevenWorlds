using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
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
        Setup();
    }

    public override Task OnClick()
    {
        try {
            LOG.Log($"Selecting character {type}");
            CharacterCreationScreenRefresherService.SetHasCharacterTypeSelected(true);
            CharacterCreationScreenRefresherService.SetSelectedCharacterType(type);
            UIEvents.ChangeGameText(GameTextId.SelectedCharacterType, type.ToString());
            return base.OnClick();
        }
        catch (System.Exception e) {
            LOG.Log(e);
            throw;
        }
    }
}
