using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Database.Models
{
    public class CharacterModel : BaseModel
    {
        public string PlayerName { get; set; }
        public List<CharacterData> Characters { get; set; }
    }
}
