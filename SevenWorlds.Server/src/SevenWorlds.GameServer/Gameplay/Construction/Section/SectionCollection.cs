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
            return new SectionBundle() {
                Armories = bundle.Armories.FindAll(x => x.AreaId == areaId),
                MonsterCamps = bundle.MonsterCamps.FindAll(x => x.AreaId == areaId),
                ProductionCamps = bundle.ProductionCamps.FindAll(x => x.AreaId == areaId),
                Shops = bundle.Shops.FindAll(x => x.AreaId == areaId),
            };
        }

        public void SetBundle(SectionBundle bundle)
        {
            this.bundle = bundle;
        }
    }
}
