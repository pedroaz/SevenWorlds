using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Section
{
    public interface ISectionCollection : IDataCollection<SectionData>
    {
        List<SectionData> FindAllSectionsByArea(string areaId);
    }
}
