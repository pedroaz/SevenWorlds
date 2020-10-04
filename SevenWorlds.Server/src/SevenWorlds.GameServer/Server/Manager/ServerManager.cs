using SevenWorlds.GameServer.Utils.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server.Manager
{
    public class ServerManager : IServerManager
    {
        private ILogService logService { get; }

        public ServerManager(ILogService logService)
        {
            this.logService = logService;
        }

        public void StartServer()
        {
            logService.Log("Starting the server");
        }
    }
}
