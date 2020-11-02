using MongoDB.Driver;
using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfigurator configurator;
        private readonly ILogService logService;
        private readonly IMongoDatabase database;
        private IMongoCollection<AccountModel> accountsCollection;
        private IMongoCollection<MasterDataModel> masterDataCollection;
        private IMongoCollection<CharacterModel> charactersCollection;
        private IMongoCollection<PlayerModel> playersCollection;

        private const string accountsDbName = "Accounts";
        private const string masterDataDbName = "MasterDatas";
        private const string charactersDbName = "Characters";
        private const string playersDbName = "Players";


        public DatabaseService(IConfigurator configurator, ILogService logService)
        {
            this.configurator = configurator;
            this.logService = logService;
            database = new MongoClient(configurator.Config.MongoDbKey).GetDatabase("SevenWorldsTestDatabase");

            accountsCollection = database.GetCollection<AccountModel>(accountsDbName);
            masterDataCollection = database.GetCollection<MasterDataModel>(masterDataDbName);
            charactersCollection = database.GetCollection<CharacterModel>(charactersDbName);
            playersCollection = database.GetCollection<PlayerModel>(playersDbName);
        }

        #region Delete
        public async Task DeleteAll()
        {
            logService.Log($"Deleting all", type: LogType.Database);
            await accountsCollection.DeleteManyAsync("{}");
            await masterDataCollection.DeleteManyAsync("{}");
            await charactersCollection.DeleteManyAsync("{}");
            await playersCollection.DeleteManyAsync("{}");
        }
        #endregion

        #region Get
        public async Task<AccountModel> GetAccountModelByPlayerName(string playerName)
        {
            logService.Log($"Getting Account Model from Database with playername: {playerName}", type: LogType.Database);
            return await accountsCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
        }

        public async Task<AccountModel> GetAccountModel(string username)
        {
            logService.Log($"Getting Account Model from Database with username: {username}", type: LogType.Database);
            return await accountsCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<MasterDataModel> GetMasterData(string serverId)
        {
            logService.Log($"Getting Master Data from Database with ServerId: {serverId}", type: LogType.Database);
            return await masterDataCollection.Find(x => x.ServerId == serverId).FirstOrDefaultAsync(); ;
        }

        public async Task<PlayerData> GetPlayerData(string playerName)
        {
            logService.Log($"Getting Player Data from Database with PlayerName: {playerName}", type: LogType.Database);
            var model = await playersCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
            return model.Data;
        }

        public async Task<List<CharacterData>> GetAllCharactersFromPlayer(string playerName)
        {
            logService.Log($"Getting all character for player: {playerName}", type: LogType.Database);
            var model = await charactersCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
            if (model == null || model.Characters == null) {
                logService.Log($"Did not found any characters for player: {playerName}", type: LogType.Database);
                return new List<CharacterData>();
            }
            logService.Log($"Found {model.Characters.Count} characters", type: LogType.Database);
            return model.Characters;
        }
        #endregion

        #region Insert
        public async Task InsertMasterData(MasterDataModel model)
        {
            await masterDataCollection.InsertOneAsync(model);
        }

        public async Task InsertAccount(AccountModel model)
        {
            await accountsCollection.InsertOneAsync(model);
        }

        public async Task InsertCharacter(string playerName, CharacterData data)
        {
            logService.Log($"Inserting character into the database for player: {playerName}");
            var model = await charactersCollection.Find(x => x.PlayerName == playerName).FirstOrDefaultAsync();
            // Adding first character
            if (model == null) {
                model = new CharacterModel() {
                    PlayerName = playerName,
                    Characters = new List<CharacterData>() {
                        data
                    }
                };
                await charactersCollection.InsertOneAsync(model);
            }
            else {
                model.Characters.Add(data);
                var filter = Builders<CharacterModel>.Filter.Eq("PlayerName", playerName);
                await charactersCollection.ReplaceOneAsync(filter, model);
            }
        }
        public async Task InsertPlayer(PlayerModel model)
        {
            await playersCollection.InsertOneAsync(model);
        }
        #endregion

        #region Update
        public async Task UpdatePlayer(PlayerData playerData)
        {
            var filter = Builders<PlayerModel>.Filter.Eq("PlayerName", playerData.PlayerName);
            var update = Builders<PlayerModel>.Update.Set("Data", playerData);
            await playersCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateMasterData(MasterDataModel model)
        {
            var filter = Builders<MasterDataModel>.Filter.Eq("ServerId", model.ServerId);
            var update = Builders<MasterDataModel>.Update
                .Set("Universes", model.Universes)
                .Set("Worlds", model.Worlds)
                .Set("Areas", model.Areas)
                .Set("Sections", model.Sections);
            await masterDataCollection.UpdateOneAsync(filter, update);
        }
        #endregion

        #region Check
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


        #endregion





    }
}
