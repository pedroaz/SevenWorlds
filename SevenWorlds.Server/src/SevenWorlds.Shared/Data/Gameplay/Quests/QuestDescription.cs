using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Quests
{
    public class QuestDescription
    {
        public string QuestId;
        public bool IsInstant;
        public Dictionary<WorldResourceType, int> ResourcesToCollect;
        public Dictionary<MonsterType, int> MonstersToKill;

        private Dictionary<MonsterType, int> monstersKilled;
        private Dictionary<WorldResourceType, int> resourcesCollected;

        public List<CharacterType> CharacterTypeRewards;
        public int MoneyReward;
        public List<string> SpiritRelicIdsReward;
        public List<string> PhysicalRelicIdsReward;

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
