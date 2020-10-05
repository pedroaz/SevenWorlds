using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Sync
{
    public class UniverseSyncData
    {
        public UniverseData Universe { get; set; }
        public List<WorldData> Worlds { get; set; }
    }
}
