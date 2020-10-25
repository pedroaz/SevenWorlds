using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum SectionType
    {
        MonsterCamp,
        Shop,
        ProductionCamp
    }

    [System.Serializable]
    public class SectionData : NetworkData
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
