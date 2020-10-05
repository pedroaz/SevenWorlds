using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Network;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SevenWorlds.ConsoleClient.App
{
    class ClientApplication
    {
        static NetworkClient client;
        static string command = "";

        static void print(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            try {
                await Connect();
                print("Connceted to the server");

                while (command != "quit") {
                    print("Welcome to the console client");
                    print("Type help for commands");
                    var line = Console.ReadLine().ToLower();
                    switch (line) {
                        case "help":
                            ShowHelp();
                            break;
                        case "set_ping_handler":
                            SetPingHandler();
                            break;
                        case "login":
                            await Login();
                            break;
                        case "chat":
                            Chat();
                            break;
                    }
                }
            }
            catch (Exception e) {
                print(e.Message);
                throw;
            }
        }

        private static void Chat()
        {
            print("What you want to say:");
            var m = Console.ReadLine();
            client.SendChatMessage(new ChatMessageData() {
                PlayerName = "Console",
                Message = m
            });
        }

        private static async Task Login()
        {
            print("Type player name");
            var playerName = Console.ReadLine();
            var logged = await client.Login(new LoginData() {
                PlayerName = playerName
            });
            if (logged) {
                print("Client was logged");
            }
            else {
                print("Client wasn't able to log");
            }
        }

        private static void SetPingHandler()
        {
            client.SetOnPingHandler((x) => {
                print("PING");
            });
        }

        private static void ShowHelp()
        {
            print("[set_ping_handler] - Log if the client is recieving pings");
            print("[login] - Login to the server");
        }

        private static async Task Connect()
        {
            client = new NetworkClient();
            await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
        }
    }
}
