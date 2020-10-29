using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public interface IGameFactory
    {
        void DumpMasterData();
        Task SetupGameServer(string serverId);
        Task SetFakeData();
    }
}
