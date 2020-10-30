﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OpenGithubButton : GameButton
{
    public override Task OnClick()
    {
        WebHelperService.OpenGitHubPage();
        return base.OnClick();
    }
}
