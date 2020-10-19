using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Sync
{
    public class AreaSyncData : NetworkData
    {
        public AreaData Area { get; set; }
        public List<CharacterData> Characters { get; set; }
        public SectionBundle Sections { get; set; }
    }
}
