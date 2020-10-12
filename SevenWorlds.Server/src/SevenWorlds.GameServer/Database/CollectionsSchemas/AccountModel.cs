using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SevenWorlds.GameServer.Database.CollectionsSchemas
{
    public class AccountModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }
    }
}
