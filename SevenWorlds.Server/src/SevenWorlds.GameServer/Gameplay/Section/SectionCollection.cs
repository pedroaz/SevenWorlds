using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Section
{
    public class SectionCollection : ISectionCollection
    {

        private SectionBundle bundle { get; set; }
        private ILogService logService { get; }

        public SectionBundle Bundle => bundle;

        public SectionCollection(ILogService logService)
        {
            bundle = new SectionBundle();
            this.logService = logService;
        }
        
        public SectionBundle FindAllSectionsByArea(string areaId)
        {
            return bundle;
        }

        public void SetBundle(SectionBundle bundle)
        {
            this.bundle = bundle;
        }
    }
}
