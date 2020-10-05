using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Sync
{
    public class WorldSyncData
    {
        public WorldData World { get; set; }
        public List<AreaData> Areas { get; set; }
    }
}
