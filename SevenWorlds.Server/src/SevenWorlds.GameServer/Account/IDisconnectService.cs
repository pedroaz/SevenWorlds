using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public interface IDisconnectService
    {
        Task DisconnectPlayer(string connectionId);
    }
}
