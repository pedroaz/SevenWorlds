﻿using Serilog;
using Serilog.Core;
using SevenWorlds.GameServer.Utils.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Log
{
    public enum LogDestination
    {
        Console,
        File,
        All
    }

    public class LogService : ILogService
    {
        Logger fileLogger;
        Logger consoleLogger;
        private readonly IConfigurator configurator;

        

        public LogService(IConfigurator configurator)
        {
            this.configurator = configurator;

            fileLogger = new LoggerConfiguration()
                .WriteTo.File(GetLogFilePath(configurator))
                .CreateLogger();

            consoleLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        private string GetLogFilePath(IConfigurator configurator)
        {
            return configurator.GetLogFilePath();
        }

        public void Log(string message)
        {
            fileLogger.Information(message);
            consoleLogger.Information(message);
        }

        public void Log(string message, LogDestination logDestination)
        {
            switch (logDestination) {
                case LogDestination.Console:
                    consoleLogger.Information(message);
                    break;
                case LogDestination.File:
                    fileLogger.Information(message);
                    break;
                case LogDestination.All:
                    Log(message);
                    break;
            }
        }

        public void Log(Exception e)
        {
            fileLogger.Information("EXCEPTION!!!");
            fileLogger.Warning(e.Message);
        }
    }
}
