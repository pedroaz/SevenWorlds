using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Talent
{
    public enum TalentId
    {
        AttackUp,
        DefenseUp,
        HpUp,
        FireUp,
    }

    public class TalentData
    {
        public TalentDescription Description;
        public int SpentPoints;

        public void ApplyTalent(CharacterData characterData)
        {
            switch (Description.TalentId) {
                case TalentId.AttackUp:
                    characterData.CombatData.Attack += SpentPoints * 5;
                    break;
                case TalentId.DefenseUp:
                    characterData.CombatData.Defense += SpentPoints * 2;
                    break;
                case TalentId.HpUp:
                    characterData.CombatData.MaxHp += SpentPoints * 10;
                    break;
                case TalentId.FireUp:
                    characterData.CombatData.Fire += SpentPoints * 1;
                    break;
            }
        }

        public TalentData(TalentDescription description)
        {
            Description = description;
        }
    }
}
