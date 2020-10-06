using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Sync;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLogger : MonoBehaviour
{
    public bool logPing;

    private void Awake()
    {
        NetworkEvents.OnChatMessageRecieved += LogChatMessage;
        NetworkEvents.OnAreaSyncRecieved += LogAreaSync;
        NetworkEvents.OnPingRecieved += LogPing;
    }

    private void OnDestroy()
    {
        NetworkEvents.OnChatMessageRecieved -= LogChatMessage;
        NetworkEvents.OnAreaSyncRecieved -= LogAreaSync;
        NetworkEvents.OnPingRecieved -= LogPing;
    }

    public void LogChatMessage(object sender, NetworkArgs<ChatMessageData> args)
    {
        if (args.Data == null) return;
        print($"{args.Data.PlayerName} send message: {args.Data.Message}");
    }

    public void LogPing(object sender, NetworkArgs<PingData> args)
    {
        if (!logPing) return;
        print("Ping Recieved");
    }

    public void LogAreaSync(object sender, NetworkArgs<AreaSyncData> args)
    {
        print("Area sync recieved");
    }
}
