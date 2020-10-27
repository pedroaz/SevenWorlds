using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
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

    public void Awake()
    {
        Object = this;
        client = new NetworkClient();
        dataFactory = new ClientRequestsDataFactory();
    }

    public async Task<bool> ConnectToServer()
    {
        try {

            await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
            LOG.Log("Connection was ok!");
            SetEventHandlers();
            return true;
        }
        catch (Exception e) {

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
    }

    private void OnDestroy()
    {
        client.Dispose();
    }

    public async Task<bool> SendChatMessage(ChatMessageData data)
    {
        return await client.SendChatMessage(data);
    }

    public async Task<UniverseSyncData> RequestUniverseSyncData()
    {
        return await client.RequestUniverseSync();
    }

    public async Task<AreaSyncData> RequestAreaSync(string areaId, string playerId)
    {
        return await client.RequestAreaSync(areaId, playerId);
    }

    public async Task<WorldSyncData> RequestWorldSyncData(string worldId)
    {
        return await client.RequestWorldSync(worldId);
    }

    public async Task<LoginResponseData> Login(LoginData data)
    {
        LOG.Log("Sending login request");
        return await client.Login(data);
    }

    public async Task<RegisterAccountResponse> Register(string username, string password, string playerName)
    {
        var data = dataFactory.CreateRegisterAccountData(username, password, playerName);
        return await client.RequestRegister(data);
    }

    public async Task<List<CharacterData>> RequestPlayerCharacters(string playerName)
    {
        var data = await  client.RequestPlayerCharacters(playerName);
        return data;
    }

    //public async Task<LoginResponseData> Login(LoginData data)
    //{
    //    return await client.Login(data);
    //}


}
