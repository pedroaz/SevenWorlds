using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Section
{
    public interface ISectionCollection
    {
        SectionBundle Bundle { get;  }
        SectionBundle FindAllSectionsByArea(string areaId);
        void SetBundle(SectionBundle bundle);
    }
}
