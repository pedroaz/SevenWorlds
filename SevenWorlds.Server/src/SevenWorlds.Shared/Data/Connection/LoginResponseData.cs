using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;

namespace SevenWorlds.Shared.Data.Connection
{
    public class LoginResponseData : NetworkData
    {
        public PlayerData PlayerData{ get; set; }
        public bool Success { get; set; }
        public UniverseSyncData UniverseSyncData { get; set; }
    }
}
