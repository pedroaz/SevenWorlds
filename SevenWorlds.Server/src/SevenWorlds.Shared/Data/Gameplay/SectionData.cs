using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class SectionData : NetworkData
    {
        public string Name;
        public SectionTypes SectionType;
        public string AreaId;
    }
}
