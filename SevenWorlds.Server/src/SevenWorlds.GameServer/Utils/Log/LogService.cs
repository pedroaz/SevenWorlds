using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Log
{
    public class LogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
