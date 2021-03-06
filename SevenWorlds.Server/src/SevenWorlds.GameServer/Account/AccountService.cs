﻿using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.Shared.Data.Gameplay;
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

        public async Task<bool> CheckLoginCredentials(string username, string password)
        {
            var account = await databaseService.GetAccountModel(username);
            if (account == null) {
                return false;
            }
            return account.Password == password;
        }

        public async Task<string> GetPlayerName(string username)
        {
            var account = await databaseService.GetAccountModel(username);
            return account?.PlayerName;
        }

        public async Task<bool> PlayerNameExists(string playerName)
        {
            return await databaseService.PlayerNameExists(playerName);
        }

        public async Task RegisterAccount(string username, string password, string playerName)
        {
            await databaseService.InsertAccount(new AccountModel() {
                PlayerName = playerName,
                Username = username,
                Password = password
            });

            await databaseService.InsertPlayer(new PlayerData(playerName) {
            });
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await databaseService.UsernameExists(username);
        }
    }
}
