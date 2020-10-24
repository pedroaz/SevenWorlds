using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Log
{
    public interface ILogService
    {
        void Log(string message, 
            LogLevel level = LogLevel.Information, LogType type = LogType.None,
            [CallerFilePath] string file = "", [CallerMemberName] string caller = "", [CallerLineNumber] int number = 0);
        void Log(Exception e,
            [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int number = 0);
    }
}
