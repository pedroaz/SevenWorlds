using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenChangerButton : GameButton
{
    public ScreenId screenId;

    public override async Task OnClick()
    {
        await ScreenChangerService.Object.ChangeScreen(screenId);
    }
}
