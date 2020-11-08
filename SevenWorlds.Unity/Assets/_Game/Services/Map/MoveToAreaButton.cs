using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToAreaButton : GameButton
{
    public override async Task OnClick()
    {
        await NetworkService.RequestMoveCurrentCharacter(MapService.SelectedArea.Id);
        await GameState.RefreshCurrentArea();
    }
}
