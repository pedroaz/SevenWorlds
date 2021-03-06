﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputService : GameService<UserInputService>
{
    private void Awake()
    {
        Object = this;
    }

    public bool shouldHandleUserInputs = false;

    public static bool ShouldHandleUserInputs { get { return Object.shouldHandleUserInputs; } set { Object.shouldHandleUserInputs = value; } }

    private void Update()
    {
        HandleUserInputs();
    }

    private void HandleUserInputs()
    {
        if (!shouldHandleUserInputs) return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PanelChangerService.TogglePanel(GamePanelId.EscMenu);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            PanelChangerService.TogglePanel(GamePanelId.Map);
        }
    }

}
