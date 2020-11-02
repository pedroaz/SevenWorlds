using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectQuestButton : GameButton
{
    public override async Task OnClick()
    {
        try {
            await NetworkService.RequestCollectQuest(GameState.PlayerName, GameState.CurrentQuest.Description.QuestId);
            QuestScreenRefresherService.ShowQuestList();
        }
        catch (System.Exception e) {
            LOG.Log(e);
            throw;
        }
    }
}
