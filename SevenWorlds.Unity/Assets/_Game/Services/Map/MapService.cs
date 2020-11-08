using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapService : GameService<MapService>
{
    public GameObject container;

    public GameText selectedAreaText;
    public GameButton moveToAreaButton;

    private AreaData selectedArea;

    public static AreaData SelectedArea { get => Object.selectedArea; set => Object.selectedArea = value; }

    private void Awake()
    {
        Object = this;
    }

    public static void Refresh()
    {
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                int value = (i * 10) + j;
                var area = GameState.CurrentWorld.Areas.Find(x => x.Position.IsEqual(i, j));
                var btn = Object.container.transform.GetChild(value);

                btn.name = $"Area ({i},{j}) - {area.Name}";
                var moveToAreaButton = btn.GetComponent<SelectAreaToMoveButton>();
                moveToAreaButton.SetArea(area);
            }
        }

        Object.selectedAreaText.SetText("No Area Selected");
        Object.moveToAreaButton.SetInteractable(false);
    }

    public static void SelectArea(AreaData area)
    {
        Object.selectedArea = area;
        Object.selectedAreaText.SetText(area.Name);
        if(area.Type != AreaType.Blocked) {
            Object.moveToAreaButton.SetInteractable(true);
        }
    }
}
