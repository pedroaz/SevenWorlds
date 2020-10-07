using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UniverseScreenRefresherService : GameService<UniverseScreenRefresherService>
{
    List<SelectWorldButton> buttons;

    private void Awake()
    {
        Object = this;
        buttons = FindObjectsOfType<SelectWorldButton>().ToList();
    }

    public async Task Refresh()
    {
        var universeData = await NetworkService.Object.RequestUniverseSyncData();

        var worlds = GameState.Object.Worlds;

        for (int i = 0; i < 7; i++) {
            var btn = buttons.Find(x => x.WorldIndex == i);
            btn?.Refresh(worlds?.Find(x => x.WorldIndex == i));
        }
    }
}
