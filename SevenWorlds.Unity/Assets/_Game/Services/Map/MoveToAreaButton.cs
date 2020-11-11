using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToAreaButton : GameButton
{
    public override async Task OnClick()
    {
        var res = await NetworkService.RequestMoveCurrentCharacter(MapService.SelectedArea.Id);
        if (res) {
            await GameState.RefreshCurrentCharacter();
            while (GameState.IsCurrentCharacterMoving) {
                await Task.Delay(1000);
            }
        }
        
        await PanelChangerService.HidePanel(GamePanelId.Map);
    }
}
