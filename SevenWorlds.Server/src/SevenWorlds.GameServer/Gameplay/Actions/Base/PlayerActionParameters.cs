using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Base
{
    public class PlayerActionParameters
    {
        public PlayerActionParameters(PlayerActionData data)
        {

        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
