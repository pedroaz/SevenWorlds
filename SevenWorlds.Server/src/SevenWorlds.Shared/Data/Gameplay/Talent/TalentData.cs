using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Talent
{
    public enum TalentType
    {
        AttackUp,
        DefenseUp
    }

    public class TalentData
    {
        public TalentType Type;
        public bool IsEnabled;
        public int Value;

        public void ApplyTalent(CharacterData characterData)
        {
            switch (Type) {
                case TalentType.AttackUp:
                    characterData.CombatData.Attack += Value;
                    break;
                case TalentType.DefenseUp:
                    characterData.CombatData.Defense += Value;
                    break;
            }
        }
    }
}
