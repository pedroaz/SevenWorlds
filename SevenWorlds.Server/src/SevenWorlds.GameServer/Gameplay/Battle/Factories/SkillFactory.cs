using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Storage.Skills;
using System.Collections.Generic;
using System.IO;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public class SkillFactory : ISkillFactory
    {
        private readonly IConfigurator configurator;
        private Dictionary<SkillId, SkillDescription> storage = new Dictionary<SkillId, SkillDescription>();

        public SkillFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public List<SkillData> CreateListOfSkillDatas(List<SkillId> ids)
        {
            var skills = new List<SkillData>();

            if (ids == null) return skills;

            foreach (var item in ids) {
                skills.Add(new SkillData(storage[item]));
            }

            return skills;
        }

        public SkillData CreateNewSkillData(SkillId id)
        {
            return new SkillData(storage[id]);
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.SkillStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<SkillId, SkillDescription>>(json);
        }
    }
}
