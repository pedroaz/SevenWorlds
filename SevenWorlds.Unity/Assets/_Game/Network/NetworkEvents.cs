using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using System;

public static class NetworkEvents
{
    public delegate void ChatMessageEventHandle(object sender, ChatMessageRecievedArgs e);
    public static event ChatMessageEventHandle OnChatMessageRecieved = delegate { };

    public static void FireChatMessageRecievedEvent(ChatMessageRecievedArgs args)
    {
        OnChatMessageRecieved(null, args);
    }

    public delegate void PingEventHandle(object sender, PingRecievedArgs e);
    public static event PingEventHandle OnPingRecieved = delegate { };

    public static void FirePingRecievedEvent(PingRecievedArgs args)
    {
        OnPingRecieved(null, args);
    }
}

public class ChatMessageRecievedArgs : EventArgs
{
    public ChatMessageData Data;
}

public class PingRecievedArgs : EventArgs
{
    public PingData Data;
}
