using SevenWorlds.Shared.Data.Gameplay.Quests;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartQuestButton : GameButton
{
    public override async Task OnClick()
    {
        await NetworkService.RequestStartQuest(GameState.PlayerName, GameState.CurrentQuest.Description.QuestId);
        await QuestScreenRefresherService.Refresh(QuestStatus.Available);
    }
}
