using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonSound
{
    None,
    Default
}

public class GameButton : SetupMonoBehaviour
{
    public ButtonSound buttonSound;
    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {

            LOG.Log($"[CLICK] - {this.GetType()}");
            switch (buttonSound) {
                case ButtonSound.Default:
                    SoundService.Object.PlaySound(SfxId.BUTTON_CLICK_1);
                    break;
            }
            _ = OnClick();

        });
        Setup();
    }

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }

    public virtual async Task OnClick()
    {
        await new Task(() => { });
    }
}
