using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Rng
{
    public interface IRandomService
    {
        int GetRandomInt(int min, int max)
    }
}
