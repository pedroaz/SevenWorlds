using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public interface IMonsterDataFactory
    {
        void SetupStorage();
        MonsterData GetMonsterData(MonsterType monsterType);
    }
}
