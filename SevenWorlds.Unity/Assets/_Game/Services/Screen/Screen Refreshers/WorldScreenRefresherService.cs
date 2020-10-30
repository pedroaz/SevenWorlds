using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldScreenRefresherService : GameService<WorldScreenRefresherService>
{
    private void Awake()
    {
        Object = this;
    }

    public async Task Refresh()
    {
        UIEvents.ChangeGameText(GameTextId.WorldName, GameState.CurrentWorld.Name);
        await NetworkService.RequestWorldSyncData(GameState.CurrentWorld.Id);
    }
}
