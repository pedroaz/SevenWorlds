using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public class MonsterDataFactory : IMonsterDataFactory
    {
        private readonly IConfigurator configurator;
        private Dictionary<MonsterType, MonsterData> storage = new Dictionary<MonsterType, MonsterData>();

        public MonsterDataFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public MonsterData GetMonsterData(MonsterType monsterType)
        {
            if (storage.ContainsKey(monsterType)) {
                return storage[monsterType];
            }

            return null;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.MonsterStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<MonsterType, MonsterData>>(json);
        }
    }
}
