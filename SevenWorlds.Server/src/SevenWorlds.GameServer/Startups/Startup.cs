using Microsoft.Owin.Cors;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Owin;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;

namespace SevenWorlds.GameServer.Startups
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterEnumSerializers();
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }

        private static void RegisterEnumSerializers()
        {
            BsonSerializer.RegisterSerializer(new EnumSerializer<WorldResourceType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<SkillTargetType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<SkillType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<SectionType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<PlayerActionType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<MonsterType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<EquipmentType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<CharacterType>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<BattleStatus>(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<TalentId>(BsonType.String));
        }
    }
}
