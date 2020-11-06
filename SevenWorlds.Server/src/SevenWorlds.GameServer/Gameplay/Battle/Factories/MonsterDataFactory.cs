using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Monster;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public class MonsterDataFactory : IMonsterDataFactory
    {
        private readonly IConfigurator configurator;
        private readonly ISkillFactory skillFactory;
        private Dictionary<MonsterType, MonsterDescription> storage = new Dictionary<MonsterType, MonsterDescription>();

        public MonsterDataFactory(IConfigurator configurator, ISkillFactory skillFactory)
        {
            this.configurator = configurator;
            this.skillFactory = skillFactory;
        }

        public MonsterData GetMonsterData(MonsterType monsterType, int monsterLevel)
        {
            if (storage.ContainsKey(monsterType)) {
                var seed = storage[monsterType];
                MonsterData data = new MonsterData(seed, skillFactory.CreateListOfSkillDatas(seed.Skills), monsterLevel);
                return data;
            }

            return null;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.MonsterStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<MonsterType, MonsterDescription>>(json);
        }
    }
}
