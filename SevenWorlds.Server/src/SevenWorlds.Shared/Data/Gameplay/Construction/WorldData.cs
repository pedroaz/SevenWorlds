using SevenWorlds.GameServer.Gameplay.Construction.Base;
using System;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class WorldData : BaseConstructionData
    {
        public string Name;
        public string UniverseId;
        public int WorldIndex;
    }
}
