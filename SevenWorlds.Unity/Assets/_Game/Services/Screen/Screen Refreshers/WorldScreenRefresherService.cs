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
        await NetworkService.Object.RequestWorldSyncData(GameState.Object.CurrentWorld.Id);
        UIEvents.ChangeGameText(GameTextId.WorldName, GameState.WorldName);
    }
}
