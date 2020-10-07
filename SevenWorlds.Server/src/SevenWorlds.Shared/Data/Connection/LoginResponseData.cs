using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.Shared.Data.Connection
{
    public class LoginResponseData : NetworkData
    {
        public PlayerData PlayerData{ get; set; }
        public bool Success { get; set; }
    }
}
