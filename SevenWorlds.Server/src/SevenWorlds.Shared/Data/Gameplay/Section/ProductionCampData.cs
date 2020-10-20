using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Section
{
    public class ProductionCampData : SectionData
    {
        public CharacterResourceType Resource { get; set; }

        public ProductionCampData(CharacterResourceType resourceType)
        {
            Resource = resourceType;
            Type = SectionType.ProductionCamp.ToString();
        }
    }
}
