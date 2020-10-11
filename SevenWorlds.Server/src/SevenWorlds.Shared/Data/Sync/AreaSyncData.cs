using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Sync
{
    public class AreaSyncData : NetworkData
    {
        public AreaData Area { get; set; }
        public List<CharacterData> Characters { get; set; }
        public List<SectionData> Sections { get; set; }
    }
}
