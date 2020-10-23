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
        buttons = Resources.FindObjectsOfTypeAll<SelectWorldButton>().ToList();
    }

    public async Task Refresh()
    {
        var universeData = await NetworkService.Object.RequestUniverseSyncData();
        var characters = await NetworkService.Object.RequestPlayerCharacters(GameState.PlayerName);

        var worlds = GameState.Object.Worlds;

        for (int i = 0; i < 7; i++) {



            var btn = buttons.Find(x => x.WorldIndex == i);
            var worldData = worlds?.Find(x => x.WorldIndex == i);
            var characterData = characters.Find(x => x.WorldId == worldData.Id);
            btn?.Refresh(worldData, characterData);
        }
    }
}
