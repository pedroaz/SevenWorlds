using MongoDB.Bson;
using MongoDB.Driver;
using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.GameServer.Utils.Config;
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

        public DatabaseService(IConfigurator configurator)
        {
            this.configurator = configurator;

            var client = new MongoClient(
                configurator.GetMongoDbKey()
            );
            var database = client.GetDatabase("SevenWorldsTestDatabase");
            var accountsCollection = database.GetCollection<AccountModel>("Accounts");
            var accounts = accountsCollection.Find(x => true).ToList();

        }
    }
}
