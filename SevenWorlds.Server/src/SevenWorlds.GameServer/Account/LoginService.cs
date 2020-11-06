using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Server;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public class LoginService : ILoginService
    {
        private readonly IAccountService accountService;
        private readonly IGameStateService gameStateService;
        private readonly ILogService logService;
        private readonly ICharacterPlacementService characterPlacementService;
        private readonly IDatabaseService databaseService;
        private readonly IServerManager serverManager;

        public LoginService(
            IAccountService accountService, 
            IGameStateService gameStateService,
            ILogService logService,
            ICharacterPlacementService characterPlacementService,
            IDatabaseService databaseService,
            IServerManager serverManager)
        {
            this.accountService = accountService;
            this.gameStateService = gameStateService;
            this.logService = logService;
            this.characterPlacementService = characterPlacementService;
            this.databaseService = databaseService;
            this.serverManager = serverManager;
        }

        public async Task<LoginResponseData> Login(LoginData data, string connectionId)
        {
            if(serverManager.GetServerStatus() != GameServerStatus.Started) {
                return new LoginResponseData() {
                    ResponseType = LoginResponseType.ServerNotStarted
                };
            }

            if (!await accountService.UsernameExists(data.Username)) {

                logService.Log($"{data.Username} was not found!");
                return new LoginResponseData() {
                    ResponseType = LoginResponseType.UsernameNotFound
                };
            }

            if (!await accountService.CheckLoginCredentials(data.Username, data.Password)) {

                logService.Log($"Incorrect password!");
                return new LoginResponseData() {
                    ResponseType = LoginResponseType.PasswordIncorrect
                };
            }

            
            var playerName = await accountService.GetPlayerName(data.Username);
            var playerData = await databaseService.GetPlayerData(playerName);
            playerData.ConnectionId = connectionId;


            // Add that object to the game
            gameStateService.AddPlayerToGame(playerData);
            var characters = await databaseService.GetAllCharactersFromPlayer(playerData.PlayerName);
            foreach (var character in characters) {
                gameStateService.AddCharacterToGame(character);
            }
            characterPlacementService.PlaceAllPlayerCharactersIntoTheGame(playerData.PlayerName);

            // Add character to areas


            logService.Log($"Account was found and player data was added to the game");

            return new LoginResponseData() {
                UniverseSyncData = gameStateService.GetUniverseSyncData(),
                PlayerData = playerData,
                ResponseType = LoginResponseType.Success
            };
        }
    }
}
