using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectWorldButton : GameButton
{
    public int WorldIndex;

    private TextMeshProUGUI btnText;

    public override void AfterAwake()
    {
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override Task OnClick()
    {
        return base.OnClick();
    }

    public void Refresh(WorldData data)
    {
        btnText.text = data.Name;
    }
}
