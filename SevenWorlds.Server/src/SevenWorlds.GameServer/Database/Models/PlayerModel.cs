using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database.Models
{
    public class PlayerModel : BaseModel
    {
        public string PlayerName { get; set; }
        public PlayerData Data { get; set; }
    }
}
