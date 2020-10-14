using SevenWorlds.GameServer.Database;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public class AccountService : IAccountService
    {
        private readonly IDatabaseService databaseService;

        public AccountService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<bool> CheckLogin(string username, string password)
        {
            var account = await databaseService.GetAccountModelByUsername(username);
            return account.Password == password;
        }

        public async Task<PlayerData> Login(string username, string connectionId)
        {
            var playerData = await databaseService.GetPlayerDataByUsername(username);
            if(playerData != null) {
                playerData.ConnectionId = connectionId;
            }
            return playerData;
        }

        public async Task<bool> PlayerNameExists(string playerName)
        {
            return await databaseService.GetAccountModelByPlayerName(playerName) != null;
        }

        public Task RegisterAccount(string username, string response, string playerName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await databaseService.GetAccountModelByUsername(username) != null;
        }
    }
}
