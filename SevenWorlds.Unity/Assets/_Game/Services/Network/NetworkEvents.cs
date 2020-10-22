using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Sync;
using System;

public static class NetworkEvents
{
    public delegate void ChatMessageEventHandle(object sender, NetworkArgs<ChatMessageData> e);
    public static event ChatMessageEventHandle OnChatMessageRecieved = delegate { };
    public static void FireChatMessageRecievedEvent(NetworkArgs<ChatMessageData> args)
    {
        OnChatMessageRecieved(null, args);
    }

    public delegate void PingEventHandle(object sender, NetworkArgs<bool> e);
    public static event PingEventHandle OnPingRecieved = delegate { };
    public static void FirePingRecievedEvent(NetworkArgs<bool> args)
    {
        OnPingRecieved(null, args);
    }

    public delegate void AreaSyncEventHandle(object sender, NetworkArgs<AreaSyncData> e);
    public static event AreaSyncEventHandle OnAreaSyncRecieved = delegate { };
    public static void FireAreaSyncEvent(NetworkArgs<AreaSyncData> args)
    {
        OnAreaSyncRecieved(null, args);
    }


}

public class NetworkArgs<T> : EventArgs
{
    public T Data;
}

