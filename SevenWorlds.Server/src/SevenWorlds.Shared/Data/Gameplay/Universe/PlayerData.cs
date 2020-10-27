using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    /// <summary>
    /// This object will not be store to the database
    /// </summary>
    [System.Serializable]
    public class PlayerData : NetworkData
    {
        public string ConnectionId;
        public string PlayerName;
        public List<string> PhysicalRelicsIds;
        public List<string> SpiritRelicIds;
        public int Money;
        public List<CharacterType> AvailableCharacters;
        public List<QuestData> Quests;

        public PlayerData(string playerName)
        {
            PlayerName = playerName;
            PhysicalRelicsIds = new List<string>();
            SpiritRelicIds = new List<string>();
            Money = 0;
            Id = Guid.NewGuid().ToString();
            TimeOfCreation = DateTime.Now;
            Quests = new List<QuestData>();
            AvailableCharacters = new List<CharacterType>();
        }
    }
}
