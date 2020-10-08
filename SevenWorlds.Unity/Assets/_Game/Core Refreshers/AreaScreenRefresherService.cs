using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class AreaScreenRefresherService : GameService<AreaScreenRefresherService>
{
    private void Awake()
    {
        Object = this;
    }

    public async Task Refresh()
    {
        var areaSyncData = await NetworkService.Object.RequestAreaSync(GameState.Object.CurrentArea.Id, GameState.Object.PlayerData.Id);
        GameState.Object.Sections = areaSyncData.Sections;
    }
}
