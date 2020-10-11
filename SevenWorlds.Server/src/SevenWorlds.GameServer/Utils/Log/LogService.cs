using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Log
{
    public class LogService : ILogService
    {
        Logger logger;

        public LogService()
        {
            logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"c:\Users\Pedro\Desktop\dev\MMO\SevenWorlds\SevenWorlds.Server\log\ServerLog.txt")
                .CreateLogger();
        }

        public void Log(string message)
        {
            logger.Information(message);
        }
    }
}
