using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenChangerButton : GameButton
{
    public ScreenId screenId;

    public override async Task OnClick()
    {
        try {
            await ScreenChangerService.ChangeScreen(screenId);
        }
        catch (System.Exception e) {

            LOG.Log(e);
        }
        
    }
}
