using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebHelperService : GameService<WebHelperService>
{
    private void Awake()
    {
        Object = this;
    }

    public void OpenGitHubPage()
    {
        Application.OpenURL("https://github.com/pedroaz/SevenWorlds");
    }
}
