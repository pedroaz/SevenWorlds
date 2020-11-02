using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay.Quests
{
    [Serializable]
    public class QuestDescription
    {
        public QuestId QuestId;
        public string QuestName;
        public string QuestTextDescription;
        public bool IsInstant;
        public Dictionary<WorldResourceType, int> ResourcesToCollect = new Dictionary<WorldResourceType, int>();
        public Dictionary<MonsterType, int> MonstersToKill = new Dictionary<MonsterType, int>();

        public List<CharacterType> CharacterTypeRewards;
        public int MoneyReward;
        public List<string> SpiritRelicIdsReward = new List<string>();
        public List<string> PhysicalRelicIdsReward = new List<string>();
    }
}
