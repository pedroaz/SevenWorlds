using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Database.Models
{
    public class PlayerModel : BaseModel
    {
        public string PlayerName { get; set; }
        public PlayerData Data { get; set; }
    }
}
