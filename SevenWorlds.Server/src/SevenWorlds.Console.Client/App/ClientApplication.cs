using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;
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

        private static PlayerData playerData { get; set; }

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

                SetDefaultHandlers();

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
                            Login();
                            break;
                        case "1":
                            Login1();
                            break;
                        case "chat":
                            Chat();
                            break;
                        case "move":
                            Move();
                            break;
                        case "register":
                            Register();
                            break;
                    }
                }
            }
            catch (Exception e) {
                print(e.Message);
                throw;
            }
        }

        

        private static void ShowHelp()
        {
            print("[set_ping_handler] - Log if the client is recieving pings");
            print("[login] - Login to the server");
            print("[register] - Register the account");
        }


        private static void SetDefaultHandlers()
        {
            client.SetOnChatMessageHandler((x) => {
                print(x.Message);
            });
        }

        private static async void Move()
        {
            await client.RequestStartPlayerAction(new PlayerMovementActionData(){
                
            });
        }

        private static async void Chat()
        {
            if(playerData == null) {
                print("Please log in first");
            }
            print("What you want to say:");
            var m = Console.ReadLine();
            await client.SendChatMessage(new ChatMessageData() {
                PlayerName = "Console",
                Message = m
            });
        }

        private static async void Login()
        {
            print("Type username");
            var username = Console.ReadLine();
            print("Type password");
            var password = Console.ReadLine();
            var response = await client.Login(new LoginData() {
                Username = username,
                Password = password
            });
            if (response.ResponseType == LoginResponseType.Success) {
                playerData = response.PlayerData;
            }
            else {
                print("Client wasn't able to log");
            }
        }

        private static async void Login1()
        {
            var username = "pedro_test";
            var password = "123";

            print($"Login with {username} and {password}");
            var response = await client.Login(new LoginData() {
                Username = username,
                Password = password
            });
            if (response.ResponseType == LoginResponseType.Success) {
                playerData = response.PlayerData;
                print($"Login was ok");
            }
            else {
                print("Client wasn't able to log");
            }
        }

        private static async void Register()
        {
            await client.RequestRegister(new RegisterAccountData(){ 
                Username = "Test",
                Password = "123",
                PlayerName = "Eu"
            });
        }

        private static void SetPingHandler()
        {
            client.SetOnPingHandler((x) => {
                print("PING");
            });
        }

        
        private static async Task Connect()
        {
            client = new NetworkClient();
            await client.Connect(NetworkConstants.ServerUrl, NetworkConstants.MainHubName);
        }
    }
}
