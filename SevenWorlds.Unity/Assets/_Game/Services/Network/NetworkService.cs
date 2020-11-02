using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkService : GameService<NetworkService>
{
    private NetworkClient client;
    private ClientRequestsDataFactory dataFactory;
    private bool isConnected;

    public static bool IsConnected { get => Object.isConnected; }

    public void Awake()
    {
        Object = this;
        client = new NetworkClient();
        dataFactory = new ClientRequestsDataFactory();
    }

    public static async Task<bool> ConnectToServer()
    {
        try {

            await Object.client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
            LOG.Log("Connection was ok!");
            Object.SetEventHandlers();
            Object.isConnected = true;
            return true;
        }
        catch (Exception e) {

            Object.isConnected = false;
            LOG.Log($"Wasn't able to connect to server. Will try again - {e.Message}", LogLevel.Warning);
            return false;
        }
        
    }

    private void SetEventHandlers()
    {
        client.SetOnChatMessageHandler(
            (data) => {
                NetworkEvents.FireChatMessageRecievedEvent(new NetworkArgs<ChatMessageData>() {
                    Data = data
                });
            }
        );

        client.SetOnPingHandler(
            (data) => {
                NetworkEvents.FirePingRecievedEvent(new NetworkArgs<bool>() {
                    Data = data
                });
            }
        );

        client.SetOnAreaSync(
            (data) => {
                NetworkEvents.FireAreaSyncEvent(new NetworkArgs<AreaSyncData>() {
                    Data = data
                });
            }
        );

        client.SetOnPlayerDataSync(
            (data) => {
                NetworkEvents.FirePlayerDataSyncEvent(new NetworkArgs<PlayerData>() {
                    Data = data
                });
            }
        );
    }

    private void OnDestroy()
    {
        client.Dispose();
    }

    public static async Task<bool> SendChatMessage(ChatMessageData data)
    {
        return await Object.client.SendChatMessage(data);
    }

    public static async Task<UniverseSyncData> RequestUniverseSyncData()
    {
        return await Object.client.RequestUniverseSync();
    }

    public static async Task<AreaSyncData> RequestAreaSync(string areaId, string playerId)
    {
        return await Object.client.RequestAreaSync(areaId, playerId);
    }

    public static async Task<WorldSyncData> RequestWorldSyncData(string worldId)
    {
        return await Object.client.RequestWorldSync(worldId);
    }

    public static async Task<LoginResponseData> Login(LoginData data)
    {
        LOG.Log("Sending login request");
        return await Object.client.Login(data);
    }

    public static async Task<RegisterAccountResponse> Register(string username, string password, string playerName)
    {
        var data = Object.dataFactory.CreateRegisterAccountData(username, password, playerName);
        return await Object.client.RequestRegister(data);
    }

    public static async Task<List<CharacterData>> RequestPlayerCharacters(string playerName)
    {
        var data = await Object.client.RequestPlayerCharacters(playerName);
        return data;
    }

    public static async Task<PlayerData> RequestPlayerData(string playerName)
    {
        var data = await Object.client.RequestPlayerData(playerName);
        return data;
    }

    public static async Task<CharacterData> RequestCreateCharacter(string playerName, string worldId, CharacterType type)
    {
        return await Object.client.RequestCreateCharacter(playerName, worldId, type);
    }

    public static async Task<List<QuestData>> RequestPlayerQuests(string playerName)
    {
        return await Object.client.RequestPlayerQuests(playerName);
    }

    public static async Task RequestStartQuest(string playerName, QuestId questId)
    {
        await Object.client.RequestStartQuest(playerName, questId);
    }

    public static async Task RequestCollectQuest(string playerName, QuestId questId)
    {
        await Object.client.RequestCollectQuest(playerName, questId);
    }

}
