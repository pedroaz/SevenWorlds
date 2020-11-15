using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Section;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Sync
{
    [System.Serializable ]
    public class AreaSyncData
    {
        public AreaData Area;
        public List<CharacterData> Characters;
        public List<MonsterData> Monsters;
        public SectionBundle Sections;
    }
}
