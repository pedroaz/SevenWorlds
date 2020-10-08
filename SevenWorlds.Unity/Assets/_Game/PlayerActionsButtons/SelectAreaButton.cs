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
        AreaData area = GameState.Object.Areas.Find(_ => (_.Position.X == x) && (_.Position.Y == y));
        GameState.Object.CurrentArea = area;
        await ScreenChangerService.Object.ChangeScreen(ScreenId.Area);
    }

    public void SetupPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
