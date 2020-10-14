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
        Task<MasterDataModel> GetMasterData(string serverId);
        Task<AccountModel> GetAccountModelByUsername(string username);
        Task<AccountModel> GetAccountModelByPlayerName(string playerName);
        Task<PlayerData> GetPlayerDataByUsername(string username);
    }
}
