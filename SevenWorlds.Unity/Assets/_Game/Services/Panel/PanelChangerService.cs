using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelChangerService : GameService<PanelChangerService>
{
    private List<GamePanel> panels;

    private void Awake()
    {
        Object = this;
        panels = Resources.FindObjectsOfTypeAll<GamePanel>().ToList();
    }

    public static void ShowPanel(GamePanelId id)
    {
        Object.panels.Find(x => x.Id == id)?.Show();
    }

    public static void HidePanel(GamePanelId id)
    {
        Object.panels.Find(x => x.Id == id)?.Hide();
    }

    public static void TogglePanel(GamePanelId id)
    {
        var panel = Object.panels.Find(x => x.Id == id);
        if (panel == null) return;
        if (panel.IsOpen) {
            panel.Hide();
        }
        else {
            panel.Show();
        }
    }
}
