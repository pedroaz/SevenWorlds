using SevenWorlds.Shared.Data.Gameplay;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectWorldButton : GameButton
{
    [HideInInspector]
    public int WorldIndex;

    private TextMeshProUGUI btnText;

    public override void AfterAwake()
    {
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override async Task OnClick()
    {
        GameState.Object.CurrentWorld = GameState.Object.Worlds.Find(x => x.WorldIndex == WorldIndex);
        await ScreenChangerService.Object.ChangeScreen(ScreenId.World);
    }

    public void Refresh(WorldData data)
    {
        btnText.text = data.Name;
    }
}
