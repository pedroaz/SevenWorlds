using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Database.Models
{
    public class MasterDataModel : BaseModel
    {
        public string ServerId { get; set; }
        public List<UniverseData> Universes { get; set; }
        public List<WorldData> Worlds { get; set; }
        public List<AreaData> Areas { get; set; }
        public SectionBundle Sections { get; set; }
    }
}
