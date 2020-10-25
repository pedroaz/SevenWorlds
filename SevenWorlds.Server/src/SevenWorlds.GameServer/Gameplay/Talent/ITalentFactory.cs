using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Talent
{
    public interface ITalentFactory
    {
        TalentBundle CreateNewBundle(CharacterType characterType);
        void SetupStorage();
    }
}
