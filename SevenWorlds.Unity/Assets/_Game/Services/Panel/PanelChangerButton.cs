using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class PanelChangerButton : GameButton
{
    public enum PanelChangerButtonType
    {
        Open,
        Close,
        Toggle
    }

    public PanelChangerButtonType Type;
    public GamePanelId Id;

    public override Task OnClick()
    {
        switch (Type) {
            case PanelChangerButtonType.Open:
                PanelChangerService.ShowPanel(Id);
                break;
            case PanelChangerButtonType.Close:
                PanelChangerService.HidePanel(Id);
                break;
            case PanelChangerButtonType.Toggle:
                PanelChangerService.TogglePanel(Id);
                break;
        }

        return base.OnClick();
    }


}
