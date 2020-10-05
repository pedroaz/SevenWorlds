using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class SectionData : NetworkData
    {
        public string Name { get; set; }
        public SectionTypes SectionType { get; set; }
        public string AreaId { get; set; }
    }
}
