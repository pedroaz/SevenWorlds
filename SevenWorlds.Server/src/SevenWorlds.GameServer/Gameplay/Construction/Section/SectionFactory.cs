using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Construction.Section
{
    public class SectionFactory : ISectionFactory
    {
        public SectionBundle CreateNewSectionBundle()
        {
            return new SectionBundle();
        }

        public MonsterCampData CreateNewMonsterCamp(MonsterType monsterType, string areaId)
        {
            var data = new MonsterCampData() {
                Type = SectionType.MonsterCamp.ToString(),
                monsterType = monsterType,
                AreaId = areaId,
            };
            data.SetupDefaultValues();
            return data;
        }

        public ProductionCampData CreateNewProductionCamp(WorldResourceType resourceType, string areaId)
        {
            var data = new ProductionCampData(){ 
                Type = SectionType.ProductionCamp.ToString(),
                Resource = resourceType,
                AreaId = areaId,
            };
            data.SetupDefaultValues();
            return data;
        }
    }
}
