using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLogger : MonoBehaviour
{
    private void Awake()
    {
        NetworkEvents.OnChatMessageRecieved += LogChatMessage;
    }

    private void OnDestroy()
    {
        NetworkEvents.OnChatMessageRecieved -= LogChatMessage;
    }

    public void LogChatMessage(object sender, ChatMessageRecievedArgs args)
    {
        if (args.Data == null) return;
        print($"{args.Data.PlayerName} send message: {args.Data.Message}");
    }
}
