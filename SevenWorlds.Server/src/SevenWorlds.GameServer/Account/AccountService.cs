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
            throw new NotImplementedException();
        }

        public async Task<PlayerData> Login(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PlayerNameExists(string username)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAccount(string username, string response, string playerName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UsernameExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}
