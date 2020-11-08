using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Sync
{
    [System.Serializable]
    public class WorldSyncData
    {
        public WorldData World;
        public List<AreaData> Areas;
    }
}
