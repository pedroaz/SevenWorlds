using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkService : GameService<NetworkService>
{
    private NetworkClient client;

    public void Awake()
    {
        Object = this;
        client = new NetworkClient();
    }

    public async Task<bool> ConnectToServer()
    {
        try {
            await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
            SetEventHandlers();
            return true;
        }
        catch (Exception e) {

            print("Wasn't able to connect to server because: ");
            print(e.Message);
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
                NetworkEvents.FirePingRecievedEvent(new NetworkArgs<PingData>() {
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

    public async Task<ChatMessageResponse> SendChatMessage(ChatMessageData data)
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
        return await client.Login(data);
    }


}
