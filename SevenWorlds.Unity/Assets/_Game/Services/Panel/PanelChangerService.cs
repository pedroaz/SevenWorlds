using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PanelChangerService : GameService<PanelChangerService>
{
    private List<GamePanel> panels;

    private void Awake()
    {
        Object = this;
        panels = Resources.FindObjectsOfTypeAll<GamePanel>().ToList();
    }

    public static void HideAllPanels()
    {
        foreach (var panel in Object.panels) {
            panel.Hide();
        }
    }

    public static async Task ShowPanel(GamePanelId id)
    {
        HideAllPanels();
        switch (id) {
            case GamePanelId.Map:
                MapService.Refresh();
                break;
        }
        Object.panels.Find(x => x.Id == id)?.Show();
    }

    public static async Task HidePanel(GamePanelId id)
    {
        Object.panels.Find(x => x.Id == id)?.Hide();
    }

    public static async Task TogglePanel(GamePanelId id)
    {
        var panel = Object.panels.Find(x => x.Id == id);
        if (panel == null) return;
        if (!panel.IsOpen) {
            await ShowPanel(id);
        }
        else {
            await HidePanel(id);
        }
    }
}
