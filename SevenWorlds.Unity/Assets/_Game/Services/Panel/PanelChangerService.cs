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

    public void ShowPanel(GamePanelId id)
    {
        panels.Find(x => x.Id == id)?.Show();
    }

    public void HidePanel(GamePanelId id)
    {
        panels.Find(x => x.Id == id)?.Hide();
    }

    public void TogglePanel(GamePanelId id)
    {
        var panel = panels.Find(x => x.Id == id);
        if (panel == null) return;
        if (panel.IsOpen) {
            panel.Hide();
        }
        else {
            panel.Show();
        }
    }
}
