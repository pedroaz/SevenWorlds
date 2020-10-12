using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public enum GameServerStatus
    {
        Initializing = 0,
        WaitingForStartRequest = 1,
        ReadyToStart = 2,
        Started = 3,
        Faulted = 4
    }

    public class ServerStatus
    {
        public GameServerStatus Status { get; set; }
        public ServerStatus()
        {
            Status = GameServerStatus.Initializing;
        }
    }
}
