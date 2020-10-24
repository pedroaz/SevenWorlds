using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum MonsterType
    {
        Poring,
        PecoPeco
    }

    public class MonsterData : NetworkData
    {
        public MonsterType MonsterType;
        public CombatData CombatData { get; set; }
    }
}
