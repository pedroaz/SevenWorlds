using Serilog;
using Serilog.Core;
using SevenWorlds.GameServer.Utils.Config;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SevenWorlds.GameServer.Utils.Log
{
    public enum LogDestination
    {
        Console,
        File,
        All
    }

    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error
    }

    public enum LogType
    {
        None,
        Initialization
    }

    public class LogService : ILogService
    {
        Logger logger;
        private readonly IConfigurator configurator;

        public LogService(IConfigurator configurator)
        {
            this.configurator = configurator;

            logger = new LoggerConfiguration()
                .WriteTo.File(GetLogFilePath(configurator))
                .WriteTo.Console()
                .CreateLogger();
        }

        private string GetLogFilePath(IConfigurator configurator)
        {
            return configurator.Config.LogFilePath;
        }

        public void Log(
            string message, LogLevel level = LogLevel.Information, LogType type = LogType.None,
            [CallerFilePath] string file = "",[CallerMemberName] string method = "", [CallerLineNumber] int number = 0)
        {
            string line = CreateLogLine(message, Path.GetFileName(file), method, number, type);

            switch (level) {
                case LogLevel.Debug:
                    logger.Debug(line);
                    break;
                case LogLevel.Information:
                    logger.Information(line);
                    break;
                case LogLevel.Warning:
                    logger.Warning(line);
                    break;
                case LogLevel.Error:
                    logger.Error(line);
                    break;
            }
        }

        private string CreateLogLine(string message,string fileName, string caller, int number, LogType type)
        {
            return $"[{fileName}@{number}->{caller}]{GetLogPrefix(type)} {message}";
        }

        private string GetLogPrefix(LogType type)
        {
            switch (type) {
                case LogType.Initialization:
                    return "[Initialization]";
                case LogType.None:
                default:
                    return "";
            }
        }

        public void Log(Exception e, 
            [CallerFilePath] string file = "", [CallerMemberName] string method = "", [CallerLineNumber] int number = 0)
        {
            Log(e.Message, level: LogLevel.Error, file: file, method: method, number: number);
        }
    }
}
