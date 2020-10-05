using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    [HideInInspector]
    public static NetworkService Object;

    private NetworkClient client;

    public void Awake()
    {
        Object = this;
        client = new NetworkClient();
    }

    // Start is called before the first frame update
    async void Start()
    {
        await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
        SetEventHandlers();
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

    public void SendChatMessage(ChatMessageData data)
    {
        client.SendChatMessage(data);
    }
}
