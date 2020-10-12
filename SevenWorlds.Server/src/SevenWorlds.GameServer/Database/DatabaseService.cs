using MongoDB.Bson;
using MongoDB.Driver;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
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

        public DatabaseService(IConfigurator configurator, ILogService logService)
        {
            this.configurator = configurator;
            this.logService = logService;
            database = new MongoClient(
                configurator.GetMongoDbKey()
            ).GetDatabase("SevenWorldsTestDatabase");

            accountsCollection = database.GetCollection<AccountModel>("Accounts");
            serverMasterDataCollection = database.GetCollection<MasterDataModel>("ServerMasterDatas");
        }

        public async Task<MasterDataModel> GetMasterData(string serverId)
        {
            logService.Log($"Getting Master Data from Database with ServerId: {serverId}");
            MasterDataModel masterData = new MasterDataModel();
            try {
                 masterData = await serverMasterDataCollection.Find(x => x.ServerId == serverId).FirstOrDefaultAsync();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
            return masterData;
        }
    }
}
