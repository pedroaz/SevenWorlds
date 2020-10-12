using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public interface IGameServerFactory
    {
        void DumpMasterData();
        Task SetupGameServer(string serverId);
        void SetupGameServerUsingFakeData();
    }
}
