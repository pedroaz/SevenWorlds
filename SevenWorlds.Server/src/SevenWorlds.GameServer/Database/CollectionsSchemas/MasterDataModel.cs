using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Database
{
    public class MasterDataModel : BaseModel
    {
        public string ServerId { get; set; }
        //private List<UniverseData> UniverseCollection { get; set; }
        //private List<WorldData> WorldCollection { get; set; }
    }
}
