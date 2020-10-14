using MongoDB.Bson;
using MongoDB.Driver;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfigurator configurator;
        private readonly ILogService logService;
        private IMongoDatabase database;
        private IMongoCollection<AccountModel> accountsCollection;
        private IMongoCollection<MasterDataModel> serverMasterDataCollection;
        private IMongoCollection<PlayerModel> playerDataCollection;

        public DatabaseService(IConfigurator configurator, ILogService logService)
        {
            this.configurator = configurator;
            this.logService = logService;
            database = new MongoClient(configurator.GetMongoDbKey()).GetDatabase("SevenWorldsTestDatabase");

            accountsCollection = database.GetCollection<AccountModel>("Accounts");
            serverMasterDataCollection = database.GetCollection<MasterDataModel>("ServerMasterDatas");
            playerDataCollection = database.GetCollection<PlayerModel>("PlayerDatas");
        }

        public async Task<AccountModel> GetAccountModelByPlayerName(string playerName)
        {
            logService.Log($"Getting Account Model from Database with playername: {playerName}");
            return await accountsCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
        }

        public async Task<AccountModel> GetAccountModelByUsername(string username)
        {
            logService.Log($"Getting Account Model from Database with username: {username}");
            return await accountsCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<MasterDataModel> GetMasterData(string serverId)
        {
            logService.Log($"Getting Master Data from Database with ServerId: {serverId}");
            return await serverMasterDataCollection.Find(x => x.ServerId == serverId).FirstOrDefaultAsync();
        }

        public async Task<PlayerData> GetPlayerDataByUsername(string username)
        {
            logService.Log($"Getting Player Data from Database with username name: {username}");
            var playerModel = await playerDataCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
            return new PlayerData(){ 
                PlayerName = playerModel.PlayerName,
                Username = playerModel.Username,
                PlayerId = playerModel.PlayerId
            };
        }
    }
}
