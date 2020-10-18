using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.GameState;
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

        public LoginService(
            IAccountService accountService, 
            IGameStateService gameStateService,
            ILogService logService,
            ICharacterPlacementService characterPlacementService)
        {
            this.accountService = accountService;
            this.gameStateService = gameStateService;
            this.logService = logService;
            this.characterPlacementService = characterPlacementService;
        }

        public async Task<LoginResponseData> Login(LoginData data, string connectionId)
        {
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

            // Create player data object
            PlayerData playerData = new PlayerData(){ 
                Id = Guid.NewGuid().ToString(),
                ConnectionId = connectionId,
                PlayerName = await accountService.GetPlayerName(data.Username),
                CharacterIds = null,
                RelicIds = null
            };
            // Add that object to the game
            gameStateService.AddPlayerToGame(playerData);
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
