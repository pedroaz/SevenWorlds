using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.ConsoleClient.App
{
    class ClientApplication
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            NetworkClient client = new NetworkClient();
            await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
            client.SendChatMessage(new Shared.Data.ChatMessageData() {
                PlayerName = "Console",
                Message = "Hi From the Console!"
            });
        }
    }
}
