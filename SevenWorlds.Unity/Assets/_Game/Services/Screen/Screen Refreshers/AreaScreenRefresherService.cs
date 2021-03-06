﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class AreaScreenRefresherService : GameService<AreaScreenRefresherService>
{
    private void Awake()
    {
        Object = this;
    }

    public static async Task Refresh()
    {
        await GameState.RefreshCurrentArea();
    }
}
