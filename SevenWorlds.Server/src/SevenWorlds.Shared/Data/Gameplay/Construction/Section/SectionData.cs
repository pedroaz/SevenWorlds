using SevenWorlds.GameServer.Gameplay.Construction.Base;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum SectionType
    {
        MonsterCamp,
        Shop,
        ProductionCamp
    }

    [System.Serializable]
    public class SectionData : BaseConstructionData
    {
        public string Name;
        public string Type;
        public string AreaId;
        public List<PlayerActionType> PossiblePlayerActions;

        public bool IsOfType(SectionType type)
        {
            return Type.Equals(type.ToString());
        }

        public virtual void Simulate()
        {

        }
    }
}
