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

    public override void Setup()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {

            LOG.Log($"[CLICK] - {this.GetType()}");
            switch (buttonSound) {
                case ButtonSound.Default:
                    SoundService.PlaySound(SfxId.BUTTON_CLICK_1);
                    break;
            }
            _ = OnClick();

        });
    }

    public void SetInteractable(bool value)
    {
        if (button == null) {
            LOG.Log($"Tryied to access null variable {gameObject.name}", LogLevel.Warning);
            return;
        }
        button.interactable = value;
    }

    public virtual async Task OnClick()
    {
        await new Task(() => { });
    }
}
