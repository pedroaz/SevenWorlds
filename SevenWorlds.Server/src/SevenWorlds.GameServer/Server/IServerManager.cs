using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public interface IServerManager
    {
        Task StartServer();
        void StartServerRequest(string serverId);
        ServerStatus GetServerStatus();
    }
}
