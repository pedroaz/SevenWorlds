using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Rng
{
    public class RandomService : IRandomService
    {
        private static Random random;

        public RandomService()
        {
            random = new Random();
        }

        public int GetRandomInt(int min, int max)
        {
            return random.Next(min, max + 1);
        }

        public bool FlipCoin()
        {
            return (random.Next(0, 2) == 0);
        }
    }
}
