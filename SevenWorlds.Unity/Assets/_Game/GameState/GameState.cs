using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using SevenWorlds.Shared.Data.Gameplay.Section;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class GameState : GameService<GameState>
{
    [Header("Player")]
    public PlayerData playerData;
    public QuestData currentQuest;

    [Header("General")]
    public UniverseData universe;
    public List<WorldData> worlds;
    public List<CharacterData> playerCharacters;

    [Header("Current")]
    public WorldSyncData currentWorld;
    public AreaSyncData currentArea;
    public CharacterData currentCharacter;

    public static PlayerData PlayerData { get => Object.playerData; set => Object.playerData = value; }
    public static string PlayerName { get => Object.playerData.PlayerName; }
    public static UniverseData Universe { get => Object.universe; set => Object.universe = value; }
    public static List<WorldData> Worlds { get => Object.worlds; set => Object.worlds = value; }
    public static WorldSyncData CurrentWorld { get => Object.currentWorld; set => Object.currentWorld = value; }
    public static AreaSyncData CurrentArea { get => Object.currentArea; private set => Object.currentArea = value; }
    public static List<CharacterData> PlayerCharacters { get => Object.playerCharacters; set => Object.playerCharacters = value; }
    public static bool HasAnyCharacterType { get => PlayerData.AvailableCharacters.Any(); }
    public static List<QuestData> QuestList { get => Object.playerData.Quests; set => Object.playerData.Quests = value; }
    public static QuestData CurrentQuest { get => Object.currentQuest; set => Object.currentQuest = value; }
    public static CharacterData CurrentCharacter { get => Object.currentCharacter; set => Object.currentCharacter = value; }

    private void Awake()
    {
        Object = this;
        NetworkEvents.OnPlayerDataSyncRecieved += PlayerDataSync;
        NetworkEvents.OnAreaSyncRecieved += AreaDataSync;
    }

    private void OnDestroy()
    {
        NetworkEvents.OnPlayerDataSyncRecieved -= PlayerDataSync;
        NetworkEvents.OnAreaSyncRecieved -= AreaDataSync;
    }

    private void PlayerDataSync(object sender, NetworkArgs<PlayerData> e)
    {
        LOG.Log("Recieved player data sync");
        PlayerData = e.Data;
    }

    private void AreaDataSync(object sender, NetworkArgs<AreaSyncData> e)
    {
        if (e.Data.Area.Id == currentCharacter.AreaId) {
            LOG.Log("*** AREA SYNC ***");
            CurrentArea = e.Data;

            UIEvents.ChangeGameText(GameTextId.AreaName, CurrentArea.Area.Name);
        }
    }

    public static async Task SetCurrentWorldByWorldIndex(int worldIndex)
    {
        string worldId = Worlds.Find(x => x.WorldIndex == worldIndex).Id;
        Object.currentWorld = await NetworkService.RequestWorldSyncData(worldId);
    }

    public static CharacterData GetCharacterByWorldId(string worldId)
    {
        return Object.playerCharacters.Find(x => x.WorldId == worldId);
    }

    public static async Task RefreshPlayerData()
    {
        PlayerData = await NetworkService.RequestPlayerData(PlayerName);
    }

    public static async Task RefreshUniverse()
    {
        UniverseSyncData data = await NetworkService.RequestUniverseSyncData();
        Universe = data.Universe;
        Worlds = data.Worlds;
    }

    public static async Task RefreshPlayerCharacters()
    {
        PlayerCharacters = await NetworkService.RequestPlayerCharacters(GameState.PlayerName);
    }

    public static async Task RefreshGameStateFromLoginResponse(LoginResponseData response)
    {
        PlayerData = response.PlayerData;
        Universe = response.UniverseSyncData.Universe;
        Worlds = response.UniverseSyncData.Worlds;
        PlayerCharacters = await NetworkService.RequestPlayerCharacters(response.PlayerData.PlayerName);
    }

    public static async Task RefreshQuestList()
    {
        QuestList = await NetworkService.RequestPlayerQuests(PlayerName);
    }

    public static async Task RefreshCurrentArea()
    {
        var dataSync = await NetworkService.RequestAreaSync(CurrentCharacter.AreaId, PlayerName);
        if(dataSync != null && dataSync?.Area != null) {
            NetworkEvents.FireAreaSyncEvent(new NetworkArgs<AreaSyncData>() {
                Data = dataSync
            });
        }
        else {
            LOG.Log("No area data returned on refresh", LogLevel.Warning);
        }
        
    }

    public static async Task RefreshCurrentCharacter()
    {
    }

}
