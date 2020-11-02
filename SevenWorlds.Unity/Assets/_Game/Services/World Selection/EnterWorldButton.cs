using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnterWorldButton : GameButton
{
    public override async Task OnClick()
    {
        await ScreenChangerService.ChangeScreen(ScreenId.Area);
    }
}
