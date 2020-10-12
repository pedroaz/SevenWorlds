using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public interface IDatabaseService
    {
        ServerMasterData GetServerMasterData(string serverId);
    }
}
