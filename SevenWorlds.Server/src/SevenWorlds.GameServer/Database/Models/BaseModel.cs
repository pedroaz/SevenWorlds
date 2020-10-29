using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SevenWorlds.GameServer.Database.Models
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
