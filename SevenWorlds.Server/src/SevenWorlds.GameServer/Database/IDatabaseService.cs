﻿using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public interface IDatabaseService
    {
        // Account
        Task<AccountModel> GetAccountModel(string username);
        Task<bool> UsernameExists(string username);
        Task<bool> PlayerNameExists(string playerName);
        Task InsertAccount(AccountModel model);

        // Player
        Task<PlayerData> GetPlayerData(string playerName);
        Task InsertPlayer(PlayerData model);
        Task UpdatePlayer(PlayerData playerData);

        // Characters
        Task<List<CharacterData>> GetAllCharactersFromPlayer(string playerName);
        Task InsertCharacter(CharacterData data);

        // Master
        Task<MasterDataModel> GetMasterData(string serverId);
        Task InsertMasterData(MasterDataModel model);
        Task UpdateMasterData(MasterDataModel model);

        // Admin
        Task DeleteAll();
    }
}
