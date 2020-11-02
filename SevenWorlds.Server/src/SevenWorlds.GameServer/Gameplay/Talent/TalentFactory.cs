using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Character;
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

        private Dictionary<TalentId, TalentDescription> storage;

        public TalentFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public TalentBundle CreateNewBundle(CharacterDescription characterDescription)
        {
            TalentBundle bundle = new TalentBundle();
            List<List<TalentId>> rows = characterDescription.Talents;
            
            foreach (List<TalentId> row in rows) {

                var talents = new List<TalentData>();

                foreach (TalentId talentId in row) {
                    var description = storage[talentId];
                    var data = new TalentData(description);
                    talents.Add(data);
                }
                bundle.TalentRows.Add(talents);
            }
            return bundle;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.TalentsStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<TalentId, TalentDescription>>(json);
        }
    }
}
