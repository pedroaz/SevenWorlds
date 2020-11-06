using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;

namespace SevenWorlds.GameServer.Gameplay.Construction.Section
{
    public interface ISectionFactory
    {
        MonsterCampData CreateNewMonsterCamp(MonsterType monsterType, string areaId);
        ProductionCampData CreateNewProductionCamp(WorldResourceType resourceType, string areaId);
        SectionBundle CreateNewSectionBundle();
    }
}
