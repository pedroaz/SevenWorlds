using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Chat
{
    public class ChatMessageData : NetworkData
    {
        public string PlayerName { get; set; }
        public string Message { get; set; }

        public ChatMessageData(string playerName, string message)
        {

        }
    }
}
