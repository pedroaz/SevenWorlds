using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.AI
{
    public interface IMonsterAIService
    {
        void Simulate(MonsterData monsterData);
    }
}
