using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    [System.Serializable]
    public class ProductionCampData : SectionData
    {
        public WorldResourceType Resource;

        public ProductionCampData(WorldResourceType resourceType)
        {
            Resource = resourceType;
            Type = SectionType.ProductionCamp.ToString();
        }
    }
}
