using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Talent
{
    [Serializable]
    public class TalentDescription
    {
        public TalentId TalentId;
        public string TextDescription;
        public int MaxPoints;
    }
}
