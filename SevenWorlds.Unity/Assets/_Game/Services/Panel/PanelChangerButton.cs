using SevenWorlds.Shared.UnityLog;
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

    public override async Task OnClick()
    {
        try {
            switch (Type) {
                case PanelChangerButtonType.Open:
                    await PanelChangerService.ShowPanel(Id);
                    break;
                case PanelChangerButtonType.Close:
                    await PanelChangerService.HidePanel(Id);
                    break;
                case PanelChangerButtonType.Toggle:
                    await PanelChangerService.TogglePanel(Id);
                    break;
            }
        }
        catch (System.Exception e) {

            LOG.Log(e);
            throw;
        }
    }


}
