using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Talent
{
    public class TalentBundle
    {
        public int MaxTalentPoints;

        public List<TalentData> AvailableTalents;

        public TalentBundle()
        {
            AvailableTalents = new List<TalentData>();
        }
    }
}
