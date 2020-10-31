using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectAreaButton : GameButton
{
    public int x;
    public int y;

    public override async Task OnClick()
    {
        //AreaData area = GameState.GetArea(x, y);
        //GameState.SetCurrentArea(area);
        //await ScreenChangerService.ChangeScreen(ScreenId.Area);
        await base.OnClick();
    }

    public void SetupPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
