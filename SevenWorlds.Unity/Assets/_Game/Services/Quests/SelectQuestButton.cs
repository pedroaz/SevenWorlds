using SevenWorlds.Shared.Data.Gameplay.Quests;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectQuestButton : GameButton
{
    public GameText questName;
    private QuestData questData;

    public void SetQuest(QuestData questData)
    {
        this.questData = questData;
        questName.SetText(questData.Description.QuestName);
    }

    public override async Task OnClick()
    {
        QuestScreenRefresherService.SelectQuest(questData);
        await base.OnClick();
    }
}
