using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Task InsertPlayer(PlayerModel model);

        // Characters
        Task<CharacterModel> GetAllCharactersFromPlayer(string playerName);
        Task InsertCharacter(string playerName, CharacterData data);

        // Master
        Task<MasterDataModel> GetMasterData(string serverId);
        Task InsertMasterData(MasterDataModel model);

        // Admin
        Task DeleteAll();
    }
}
