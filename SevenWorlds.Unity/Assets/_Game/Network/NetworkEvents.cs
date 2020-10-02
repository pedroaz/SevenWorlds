using SevenWorlds.Shared.Data;
using System;

public static class NetworkEvents
{
    public delegate void ChatMessageEventHandle(object sender, ChatMessageRecievedArgs e);
    public static event ChatMessageEventHandle OnChatMessageRecieved = delegate { };

    public static void FireChatMessageRecievedEvent(ChatMessageRecievedArgs args)
    {
        OnChatMessageRecieved(null, args);
    }
}

public class ChatMessageRecievedArgs : EventArgs
{
    public ChatMessageData Data;
}
