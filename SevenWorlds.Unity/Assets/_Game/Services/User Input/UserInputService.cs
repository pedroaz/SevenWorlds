using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputService : GameService<UserInputService>
{
    private void Awake()
    {
        Object = this;
    }

    public bool ShouldHandleUserInputs = false;

    private void Update()
    {
        HandleUserInputs();
    }

    private void HandleUserInputs()
    {
        if (!ShouldHandleUserInputs) return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PanelChangerService.Object.TogglePanel(GamePanelId.EscMenu);
        }
    }
}
