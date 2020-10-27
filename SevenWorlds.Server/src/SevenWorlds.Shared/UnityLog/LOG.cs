using System;
using UnityEngine;

namespace SevenWorlds.Shared.UnityLog
{
    public enum LogLevel
    {
        Information,
        Warning,
        Error
    }

    public enum LogType
    {
        None,
        Initialization,
    }

    public static class LOG
    {
        public static void Log(object obj, LogLevel level = LogLevel.Information, LogType type = LogType.None)
        {
            string line = CreateLogLine(obj.ToString(), type);

            switch (level) {
                case LogLevel.Information:
                    Debug.Log(line);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(line);
                    break;
                case LogLevel.Error:
                    Debug.LogError(line);
                    break;
            }
        }

        public static void Log(Exception e)
        {
            Log(e.Message, LogLevel.Error);
        }

        private static string CreateLogLine(string message, LogType type)
        {
            return $"{GetLogPrefix(type)} {message}";
        }

        private static string GetLogPrefix(LogType type)
        {
            switch (type) {
                case LogType.Initialization:
                    return "[Initialization]";
                case LogType.None:
                default:
                    return "";
            }
        }
    }
}
