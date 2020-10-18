using SevenWorlds.GameServer.Database.CollectionsSchemas;
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
        Task UpdateAccount(AccountModel model);

        // Master
        Task<MasterDataModel> GetMasterData(string serverId);
        Task UpdateMasterData(MasterDataModel model);

        // Admin
        Task DeleteAll();
    }
}
