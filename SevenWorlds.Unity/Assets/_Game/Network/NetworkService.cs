using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
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
                NetworkEvents.FireChatMessageRecievedEvent(new ChatMessageRecievedArgs() {
                    Data = data
                });
            }
        );

        client.SetOnPingHandler(
            (data) => {
                NetworkEvents.FirePingRecievedEvent(new PingRecievedArgs() {
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
