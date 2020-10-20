using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Factories
{
    public interface IMonsterDataFactory
    {
        void SetupDictionary();
        MonsterData GetMonsterData(MonsterType monsterType);
    }
}
