﻿using SevenWorlds.Shared.Data.Gameplay.Players;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class PlayerData
    {
        public string Id;
        public string ConnectionId;
        public string PlayerName;
        public List<string> PhysicalRelicsIds;
        public List<string> SpiritRelicIds;
        public int Money;
        public List<CharacterType> AvailableCharacters;
        public List<QuestData> Quests;
        public PlayerRecord Record;

        public PlayerData(string playerName)
        {
            PlayerName = playerName;
            PhysicalRelicsIds = new List<string>();
            SpiritRelicIds = new List<string>();
            Money = 0;
            Id = Guid.NewGuid().ToString();
            Quests = new List<QuestData>();
            AvailableCharacters = new List<CharacterType>();
            Record = new PlayerRecord();
        }

        public void KillMonster(MonsterType type, int amount)
        {
            foreach (var quest in Quests) {
                quest.KillMonster(type, amount);
            }
        }

        public void CollectResource(WorldResourceType type, int amount)
        {
            foreach (var quest in Quests) {
                quest.CollectResource(type, amount);
            }
        }

        public void StartQuest(QuestId questId)
        {
            var quest = Quests.Find(x => x.Description.QuestId == questId);
            if (quest != null && quest.Status == QuestStatus.Available) {
                quest.Status = QuestStatus.Ongoing;
                quest.RefreshStatus();
            }
        }

        public bool CollectQuest(QuestId questId)
        {
            var quest = Quests.Find(x => x.Description.QuestId == questId);
            if (quest != null && quest.Status == QuestStatus.Completed) {

                var description = quest.Description;

                // Money
                Money = description.MoneyReward;

                // Character types
                foreach (var characterType in description.CharacterTypeRewards) {
                    if (!AvailableCharacters.Contains(characterType)) {
                        AvailableCharacters.Add(characterType);
                    }
                }

                // Spirit Relics
                foreach (var relic in description.SpiritRelicIdsReward) {
                    SpiritRelicIds.Add(relic);
                }

                // Physicas Relics
                foreach (var relic in description.PhysicalRelicIdsReward) {
                    PhysicalRelicsIds.Add(relic);
                }


                quest.Status = QuestStatus.Collected;

                return true;
            }

            return false;
        }
    }
}
