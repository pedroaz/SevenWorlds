using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Storage.Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public class SkillFactory : ISkillFactory
    {
        private readonly IConfigurator configurator;
        private Dictionary<SkillType, SkillDescription> storage = new Dictionary<SkillType, SkillDescription>();

        public SkillFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public List<SkillData> CreateListOfSkillDatas(List<SkillType> types)
        {
            var skills = new List<SkillData>();

            if (types == null) return skills;

            foreach (var item in types) {
                skills.Add(new SkillData(storage[item]));
            }

            return skills;
        }

        public SkillData CreateNewSkillData(SkillType type)
        {
            return new SkillData(storage[type]);
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.SkillStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<SkillType, SkillDescription>>(json);
        }
    }
}
