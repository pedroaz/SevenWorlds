using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterCreationScreenRefresherService : GameService<CharacterCreationScreenRefresherService>
{

    public bool hasCharacterTypeSelected = false;
    public CharacterType selectedCharacterType;

    public Transform container;
    public GameObject buttonPrefab;

    private void Awake()
    {
        Object = this;
        if(container == null) {
            LOG.Log("No container on CharacterCreationScreenRefresherService", LogLevel.Error);
        }
    }

    public void Refresh()
    {
        hasCharacterTypeSelected = false;
        UIEvents.ChangeGameText(GameTextId.SelectedCharacterType, "No Char Selected");

        for (int i = 0; i < container.childCount; i++) {
            Destroy(container.GetChild(i).gameObject);
        }

        foreach (var character in GameState.PlayerData.AvailableCharacters) {
            GameObject obj = Instantiate(buttonPrefab, container);
            obj.GetComponent<SelectCharacterTypeButton>().Setup(character);
        }
    }

    public static bool GetHasCharacterTypeSelected()
    {
        return Object.hasCharacterTypeSelected;
    }

    public static CharacterType GetSelectedCharacterType()
    {
        return Object.selectedCharacterType;
    }

    public static void SetHasCharacterTypeSelected(bool value)
    {
        Object.hasCharacterTypeSelected = value;
    }

    public static void SetSelectedCharacterType(CharacterType type)
    {
        Object.selectedCharacterType = type;
    }
}
