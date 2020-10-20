using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Factories
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

        public void SetupDictionary()
        {
            var filePath = configurator.Config.MonsterCsvPath;
        }
    }
}
