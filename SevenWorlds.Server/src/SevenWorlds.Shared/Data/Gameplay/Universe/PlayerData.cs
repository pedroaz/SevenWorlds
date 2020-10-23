using SevenWorlds.Shared.Data.Base;
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
        public List<string> CharacterIds;
        public List<string> RelicIds;
    }
}
