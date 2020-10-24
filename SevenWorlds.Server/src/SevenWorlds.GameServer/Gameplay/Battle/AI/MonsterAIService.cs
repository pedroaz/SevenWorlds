using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.AI
{
    public class MonsterAIService : IMonsterAIService
    {
        public void Simulate(MonsterData monsterData)
        {
            switch (monsterData.MonsterType) {
                case MonsterType.Poring:
                    SimulatePoring(monsterData);
                    break;
                case MonsterType.PecoPeco:
                    break;
            }
        }

        private void SimulatePoring(MonsterData monsterData)
        {

        }
    }
}
