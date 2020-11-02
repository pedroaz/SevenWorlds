using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Quests
{
    public class QuestFactory : IQuestFactory
    {
        private readonly IConfigurator configurator;
        private Dictionary<QuestId, QuestDescription> storage = new Dictionary<QuestId, QuestDescription>();

        public QuestFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public QuestData CreateNewQuest(QuestId id)
        {
            if (!storage.ContainsKey(id)) return null;

            QuestData data = new QuestData(storage[id]);

            return data;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.QuestStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<QuestId, QuestDescription>>(json);
        }
    }
}
