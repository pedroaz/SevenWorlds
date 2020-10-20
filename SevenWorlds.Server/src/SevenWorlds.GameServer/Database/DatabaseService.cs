using MongoDB.Driver;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfigurator configurator;
        private readonly ILogService logService;
        private IMongoDatabase database;
        private IMongoCollection<AccountModel> accountsCollection;
        private IMongoCollection<MasterDataModel> masterDataCollection;

        private const string accountsDbName = "Accounts";
        private const string masterDataDbName = "MasterDatas";


        public DatabaseService(IConfigurator configurator, ILogService logService)
        {
            this.configurator = configurator;
            this.logService = logService;
            database = new MongoClient(configurator.Config.MongoDbKey).GetDatabase("SevenWorldsTestDatabase");

            accountsCollection = database.GetCollection<AccountModel>(accountsDbName);
            masterDataCollection = database.GetCollection<MasterDataModel>(masterDataDbName);
        }

        public async Task DeleteAll()
        {
            await accountsCollection.DeleteManyAsync("{}");
            await masterDataCollection.DeleteManyAsync("{}");
        }

        public async Task<AccountModel> GetAccountModelByPlayerName(string playerName)
        {
            logService.Log($"Getting Account Model from Database with playername: {playerName}");
            return await accountsCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
        }

        public async Task<AccountModel> GetAccountModel(string username)
        {
            logService.Log($"Getting Account Model from Database with username: {username}");
            return await accountsCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<MasterDataModel> GetMasterData(string serverId)
        {
            logService.Log($"Getting Master Data from Database with ServerId: {serverId}");
            var masterData = await masterDataCollection.Find(x => x.ServerId == serverId).FirstOrDefaultAsync();
            return masterData;
        }

        public async Task UpdateMasterData(MasterDataModel model)
        {
            await masterDataCollection.InsertOneAsync(model);
        }

        public async Task UpdateAccount(AccountModel model)
        {
            await accountsCollection.InsertOneAsync(model);
        }

        public async Task<bool> UsernameExists(string username)
        {
            var account = await accountsCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
            return account != null;
        }

        public async Task<bool> PlayerNameExists(string playerName)
        {
            var account = await accountsCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
            return account != null;
        }
    }
}
