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
        public string ConnectionId { get; set; }
        public string PlayerName { get; set; }
        public List<string> CharacterIds { get; set; }
        public List<string> RelicIds { get; set; }
    }
}
