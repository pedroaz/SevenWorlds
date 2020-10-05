using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Chat
{
    public class ChatMessageData : NetworkData
    {
        public string PlayerName { get; set; }
        public string Message { get; set; }
    }
}
