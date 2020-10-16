using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Connection;
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

        public LoginService(
            IAccountService accountService, 
            IGameStateService gameStateService,
            ILogService logService)
        {
            this.accountService = accountService;
            this.gameStateService = gameStateService;
            this.logService = logService;
        }

        public async Task<LoginResponseData> Login(LoginData data, string connectionId)
        {
            if (!await accountService.UsernameExists(data.Username)) {

                logService.Log($"{data.Username} was not found!");
                return new LoginResponseData() {
                    ResponseType = LoginResponseType.UsernameNotFound
                };
            }

            if (!await accountService.CheckLogin(data.Username, data.Password)) {

                logService.Log($"Incorrect password!");
                return new LoginResponseData() {
                    ResponseType = LoginResponseType.PasswordIncorrect
                };
            }

            var playerData = await accountService.Login(data.Username, connectionId);
            gameStateService.AddPlayerDataToGame(playerData);

            logService.Log($"Account was found");

            return new LoginResponseData() {
                UniverseSyncData = gameStateService.GetUniverseSyncData(),
                PlayerData = playerData,
                ResponseType = LoginResponseType.Success
            };
        }
    }
}
