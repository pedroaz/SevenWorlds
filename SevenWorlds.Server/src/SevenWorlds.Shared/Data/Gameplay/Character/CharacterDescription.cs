using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Character
{
    public class CharacterDescription
    {
        public CharacterType CharacterType;
        public List<List<TalentId>> Talents;
    }
}
