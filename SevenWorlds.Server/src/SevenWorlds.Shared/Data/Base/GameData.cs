using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Base
{
    public class GameData
    {
        public readonly string ObjectId = Guid.NewGuid().ToString();
    }
}
