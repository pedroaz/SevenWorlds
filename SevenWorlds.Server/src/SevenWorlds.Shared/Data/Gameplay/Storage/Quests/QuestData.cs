using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Quests
{
    public enum QuestId
    {
        InitialQuest
    }

    public enum QuestStatus
    {
        Available,
        Ongoing,
        Completed,
        Collected
    }

    [Serializable]
    public class QuestData
    {
        public QuestStatus Status;
        public QuestDescription Description;

        private Dictionary<MonsterType, int> monstersKilled = new Dictionary<MonsterType, int>();
        private Dictionary<WorldResourceType, int> resourcesCollected = new Dictionary<WorldResourceType, int>();

        public QuestData(QuestDescription description)
        {
            Description = description;

            foreach (var kvp in description.MonstersToKill) {
                monstersKilled.Add(kvp.Key, 0);
            }

            foreach (var kvp in description.ResourcesToCollect) {
                resourcesCollected.Add(kvp.Key, 0);
            }

            Status = QuestStatus.Available;
        }

        public void RefreshStatus()
        {
            if (Status == QuestStatus.Ongoing && Description.IsInstant) {
                Status = QuestStatus.Completed;
            }
        }

        public void KillMonster(MonsterType type, int amount)
        {
            if (monstersKilled.ContainsKey(type)) {
                monstersKilled[type] += amount;
            }
        }

        public void CollectResource(WorldResourceType type, int amount)
        {
            if (resourcesCollected.ContainsKey(type)) {
                resourcesCollected[type] += amount;
            }
        }
    }
}
