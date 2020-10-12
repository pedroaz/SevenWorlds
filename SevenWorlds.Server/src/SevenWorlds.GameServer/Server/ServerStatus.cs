using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Server
{
    public enum GameServerStatus
    {
        Starting,
        Started,
        GettingMasterData,
        Simulating,
        Stopping
    }

    public class ServerStatus
    {
        public GameServerStatus Status { get; set; }
    }
}
