using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Executor
{
    public interface IBattleSimulator
    {
        void SimulateBattles();
    }
}
