using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Chat
{
    public class ChatMessageData : NetworkData
    {
        public string PlayerName;
        public string Message;

        public ChatMessageData(string playerName, string message)
        {

        }
    }
}
