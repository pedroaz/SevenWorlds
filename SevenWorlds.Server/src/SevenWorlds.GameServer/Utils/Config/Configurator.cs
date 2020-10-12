using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Config
{
    public class Configurator : IConfigurator
    {
        private readonly ILogService logService;
        private ServerConfigurations config;

        public string GetLogFilePath()
        {
            return config.LogFilePath;
        }

        public string GetMongoDbKey()
        {
            return config.MongoDbKey;
        }

        public string GetServerId()
        {
            return config.ServerId;
        }

        public void ReadConfigurations(string configFilePath)
        {
            config = JsonConvert.DeserializeObject<ServerConfigurations>(File.ReadAllText(configFilePath));
        }

        public bool ShouldAutoStart()
        {
            return config.AutoStart;
        }
    }
}
