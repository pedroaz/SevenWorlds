using SevenWorlds.Shared.Data.Gameplay.Quests;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectQuestStatusButton : GameButton
{
    public QuestStatus status;

    public override async Task OnClick()
    {
        await QuestScreenRefresherService.Refresh(status);
    }
}
