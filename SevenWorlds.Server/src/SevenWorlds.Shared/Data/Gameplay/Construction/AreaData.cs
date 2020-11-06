using SevenWorlds.GameServer.Gameplay.Construction.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum AreaType
    {
        City,
        Field
    }

    [System.Serializable]
    public class AreaData : BaseConstructionData
    {
        public string Name;
        public Position Position;
        public string WorldId;
        public AreaType Type;
    }
}
