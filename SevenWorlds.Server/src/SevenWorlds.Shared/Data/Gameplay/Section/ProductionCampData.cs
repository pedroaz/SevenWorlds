using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    public class ProductionCampData : SectionData
    {
        public CharacterResourceType Resource;

        public ProductionCampData(CharacterResourceType resourceType)
        {
            Resource = resourceType;
            Type = SectionType.ProductionCamp.ToString();
        }
    }
}
