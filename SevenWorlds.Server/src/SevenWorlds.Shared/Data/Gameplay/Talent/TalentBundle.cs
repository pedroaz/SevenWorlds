using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Talent
{
    public class TalentBundle
    {
        public List<List<TalentData>> TalentRows = new List<List<TalentData>>();

        public int GetAmountOfSpentTalentPoints()
        {
            int amount = 0;

            foreach (var row in TalentRows) {
                foreach (var data in row) {
                    amount += data.SpentPoints;
                }
            }

            return amount;
        }

        public TalentBundle()
        {

        }
    }
}
