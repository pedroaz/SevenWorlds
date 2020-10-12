using Serilog;
using Serilog.Core;
using SevenWorlds.GameServer.Utils.Config;
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
        private readonly IConfigurator configurator;

        public LogService(IConfigurator configurator)
        {
            this.configurator = configurator;

            logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(configurator.GetLogFilePath())
                .CreateLogger();
        }

        public void Log(string message)
        {
            logger.Information(message);
        }

        public void Log(Exception e)
        {
            logger.Information("EXCEPTION!!!");
            logger.Warning(e.Message);
        }
    }
}
