using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class QuestScreenRefresherService : GameService<QuestScreenRefresherService>
{
    private StartQuestButton startQuestButton;
    private CollectQuestButton collectQuestButton;
    public GameObject selectQuestButtonPrefab;
    public GameScrollView questListScrollView;
    public GameObject questListContainer;
    public GameObject selectedQuestContainer;

    
    public GameText selectedQuestName;
    public GameText selectedQuestDescription;

    private QuestStatus status;

    private void Awake()
    {
        Object = this;
        startQuestButton = Resources.FindObjectsOfTypeAll<StartQuestButton>().FirstOrDefault();
        collectQuestButton = Resources.FindObjectsOfTypeAll<CollectQuestButton>().FirstOrDefault();
        NetworkEvents.OnPlayerDataSyncRecieved += RefreshWhenPlayerDataRecieved;
    }

    private void OnDestroy()
    {
        NetworkEvents.OnPlayerDataSyncRecieved -= RefreshWhenPlayerDataRecieved;
    }

    private void RefreshWhenPlayerDataRecieved(object sender, NetworkArgs<PlayerData> e)
    {
        MainThreadHelperService.AddJob(() => {
            try {
                Object.RefreshQuestList(status);
            }
            catch (Exception ex) {
                LOG.Log(ex);
                throw;
            }
        });

        
    }

    public static async Task Refresh(QuestStatus status)
    {
        await GameState.RefreshQuestList();
        Object.RefreshQuestList(status);
    }

    private void RefreshQuestList(QuestStatus status)
    {
        LOG.Log($"Refreshing quest screen with status: {status}");
        UIEvents.ChangeGameText(GameTextId.SelectedQuestStatus, $"{status} quests");
        ShowQuestList();

        Object.status = status;

        Object.questListScrollView.Clear();

        var filteredQuestList = GameState.QuestList.FindAll(x => x.Status == status);
        foreach (var quest in filteredQuestList) {
            Object.CreateQuestButton(quest);
        }
    }

    public static void RefreshSelectedQuest(QuestData questData)
    {
        ShowHideButtons();
        Object.selectedQuestName.SetText(questData.Description.QuestName);
        SetQuestText(questData);
        ShowSelectedQuest();
    }

    public static void ShowQuestList()
    {
        Object.questListContainer.SetActive(true);
        Object.selectedQuestContainer.SetActive(false);
    }

    private static void ShowSelectedQuest()
    {
        Object.questListContainer.SetActive(false);
        Object.selectedQuestContainer.SetActive(true);
    }

    public void CreateQuestButton(QuestData data)
    {
        if(selectQuestButtonPrefab == null) {
            LOG.Log("Select button prefab is null", LogLevel.Error);
        }
        var go = Instantiate(selectQuestButtonPrefab, questListScrollView.contentContainer, false);
        var btn = go.GetComponent<SelectQuestButton>();
        btn.Setup();
        btn.SetQuest(data);
    }

    

    private static void SetQuestText(QuestData questData)
    {
        string questText = "";
        switch (Object.status) {
            case QuestStatus.Available:
            case QuestStatus.Ongoing:
                questText = questData.Description.InitialDescription;
                break;
            case QuestStatus.Completed:
            case QuestStatus.Collected:
                questText = questData.Description.CompletedDescription;
                break;
        }
        Object.selectedQuestDescription.SetText(questText);
    }

    private static void ShowHideButtons()
    {
        bool showStart = false;
        bool showCollect = false;

        switch (Object.status) {
            case QuestStatus.Available:
                showStart = true;
                break;
            case QuestStatus.Ongoing:
                break;
            case QuestStatus.Completed:
                showCollect = true;
                break;
            case QuestStatus.Collected:
                break;
        }

        Object.startQuestButton.gameObject.SetActive(showStart);
        Object.collectQuestButton.gameObject.SetActive(showCollect);
    }
}
