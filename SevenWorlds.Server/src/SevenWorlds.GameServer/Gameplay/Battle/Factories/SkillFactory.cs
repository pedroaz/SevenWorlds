using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay.Skills;
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
        private Dictionary<SkillType, SkillData> storage = new Dictionary<SkillType, SkillData>();

        public SkillFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public List<SkillData> GetListOfSkillDatas(List<SkillType> types)
        {
            var skills = new List<SkillData>();
            foreach (var item in types) {
                skills.Add(storage[item]);
            }

            return skills;
        }

        public SkillData GetSkillData(SkillType type)
        {
            return storage[type];
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.SkillStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<SkillType, SkillData>>(json);
        }
    }
}
