using SevenWorlds.GameServer.Database.CollectionsSchemas;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Database
{
    public class MasterDataModel : BaseModel
    {
        public string ServerId { get; set; }
        public List<UniverseData> UniverseCollection { get; set; }
        public List<WorldData> WorldCollection { get; set; }
        public List<AreaData> AreaCollection { get; set; }
        public List<SectionData> SectionCollection { get; set; }
    }
}
