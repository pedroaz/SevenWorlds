using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class PlayerData : NetworkData
    {
        public string PlayerName;
        public string ConnectionId;
    }
}
