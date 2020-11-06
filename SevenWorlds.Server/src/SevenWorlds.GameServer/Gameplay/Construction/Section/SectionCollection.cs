using SevenWorlds.GameServer.Gameplay.Construction.Section;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay.Section;

namespace SevenWorlds.GameServer.Gameplay.Section
{
    public class SectionCollection : ISectionCollection
    {

        private SectionBundle bundle { get; set; }
        private ILogService logService { get; }

        public SectionBundle Bundle => bundle;

        public SectionCollection(ILogService logService)
        {
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
