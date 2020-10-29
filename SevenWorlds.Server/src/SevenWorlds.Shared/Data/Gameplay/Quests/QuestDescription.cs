using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Quests
{
    public class QuestDescription
    {
        public string QuestId;
        public bool IsInstant;
        public Dictionary<WorldResourceType, int> ResourcesToCollect;
        public Dictionary<MonsterType, int> MonstersToKill;

        private Dictionary<MonsterType, int> monstersKilled = new Dictionary<MonsterType, int>();
        private Dictionary<WorldResourceType, int> resourcesCollected = new Dictionary<WorldResourceType, int>();

        public List<CharacterType> CharacterTypeRewards;
        public int MoneyReward;
        public List<string> SpiritRelicIdsReward = new List<string>();
        public List<string> PhysicalRelicIdsReward = new List<string>();

        public void Setup()
        {

        }

        public void KillMonster(MonsterType type, int amount)
        {
            if (monstersKilled.ContainsKey(type)) {
                monstersKilled[type] += amount;
            }
        }

        public void GetResource(WorldResourceType type, int amount)
        {
            if (resourcesCollected.ContainsKey(type)) {
                resourcesCollected[type] += amount;
            }
        }
    }
}
