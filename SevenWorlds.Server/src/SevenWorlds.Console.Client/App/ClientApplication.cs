using SevenWorlds.GameClient.Client;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Network;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenWorlds.ConsoleClient.App
{
    class ClientApplication
    {
        static NetworkClient client;
        static string command = "";

        private static PlayerData playerData { get; set; }
        private static CharacterData characterData { get; set; }

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
                print("Welcome to the console client");
                print("Type help for commands");
                while (command != "quit") {

                    var line = Console.ReadLine().ToLower();
                    switch (line) {
                        case "h":
                            ShowHelp();
                            break;
                        case "ping":
                            SetPingHandler();
                            break;
                        case "l":
                            await Login();
                            break;
                        case "1":
                            await Login1();
                            break;
                        case "c":
                            Chat();
                            break;
                        case "m":
                            Move();
                            break;
                        case "r":
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
            print("[H] - Show Help Commands");
            print("[PING] - Log if the client is recieving pings");
            print("[L] - Login to the server");
            print("[1] - Login with account \"one\"");
            print("[C] - Chat with all players");
            print("[M] - Move the character");
            print("[R] - Register the account");
        }


        private static void SetDefaultHandlers()
        {
            client.SetOnChatMessageHandler((x) => {
                print(x.Message);
            });
        }

        private static async Task Move()
        {
            if (characterData == null) {
                print("No chracter data! Can't move!");
                return;
            }
            print("Requesting sync");
            var world = await client.RequestWorldSync(characterData.WorldId);
            var toAreaId = world.Areas[1].Id;
            print($"Moving to AreaId: {toAreaId}");
            await client.RequestMovementAction(characterData.Id, characterData.AreaId, toAreaId);
        }

        private static async void Chat()
        {
            if (playerData == null) {
                print("Please log in first");
            }
            print("What you want to say:");
            var m = Console.ReadLine();
            await client.SendChatMessage(new ChatMessageData("Console", m));
        }

        private static async Task Login()
        {
            print("Type username");
            var username = Console.ReadLine();
            print("Type password");
            var password = Console.ReadLine();
            var response = await client.Login(new LoginData(username, password));
            if (response.ResponseType == LoginResponseType.Success) {
                playerData = response.PlayerData;
                print($"Login was ok");
            }
            else {
                print("Client wasn't able to log");
            }
        }

        private static async Task Login1()
        {
            var username = "pedroaz";
            var password = "pedroaz123";

            print($"Login with {username} and {password}");
            var response = await client.Login(new LoginData(username, password));
            if (response.ResponseType == LoginResponseType.Success) {
                playerData = response.PlayerData;
                print($"Login was ok");
                print($"Requesting characters from player data: {playerData.PlayerName}");
                List<CharacterData> characters = await client.RequestPlayerCharacters(playerData.PlayerName);
                characterData = characters[0];
            }
            else {
                print("Client wasn't able to log");
            }
        }

        private static async void Register()
        {
            await client.RequestRegister(new RegisterAccountData() {
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
