using Microsoft.Owin.Cors;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Owin;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Startups
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<WorldResourceType>(BsonType.String));
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
