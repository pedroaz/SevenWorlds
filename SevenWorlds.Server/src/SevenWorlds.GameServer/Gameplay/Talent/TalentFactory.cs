using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Talent
{
    public class TalentFactory : ITalentFactory
    {
        private readonly IConfigurator configurator;

        private Dictionary<CharacterType, List<TalentData>> storage;

        public TalentFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public TalentBundle CreateNewBundle(CharacterType characterType)
        {
            TalentBundle bundle = new TalentBundle {
                AvailableTalents = storage[characterType]
            };
            return bundle;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.TalentsStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<CharacterType, List<TalentData>>>(json);
        }
    }
}
