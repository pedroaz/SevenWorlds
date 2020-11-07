using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnterWorldButton : GameButton
{
    public override async Task OnClick()
    {
        try {
            await ScreenChangerService.ChangeScreen(ScreenId.Area);
        }
        catch (System.Exception e) {
            LOG.Log(e);
            throw;
        }
        
    }
}
