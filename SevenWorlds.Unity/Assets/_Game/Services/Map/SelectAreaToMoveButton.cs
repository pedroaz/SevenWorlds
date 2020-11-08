using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectAreaToMoveButton : GameButton
{
    public AreaData area;
    public GameText gameText;

    public override async Task OnClick()
    {
        MapService.SelectArea(area);
    }

    public void SetArea(AreaData areaData){

        area = areaData;
        gameText.SetText($"({areaData.Position.X},{area.Position.Y})");
    }



}
