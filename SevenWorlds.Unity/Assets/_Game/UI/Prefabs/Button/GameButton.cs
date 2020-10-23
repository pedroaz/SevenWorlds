using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : SetupMonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            LOG.Log($"[CLICK] - {this.GetType()}");
            _ = OnClick();
        });
        Setup();
    }

    public virtual async Task OnClick()
    {
        await new Task(() => { });
    }
}
