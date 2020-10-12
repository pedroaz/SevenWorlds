using Newtonsoft.Json;
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
        private ServerConfigurations config;

        public string GetLogFilePath()
        {
            return config.LogFilePath;
        }

        public string GetMongoDbKey()
        {
            return config.MongoDbKey;
        }

        public void ReadConfigurations(string configFilePath)
        {
            config = JsonConvert.DeserializeObject<ServerConfigurations>(File.ReadAllText(configFilePath));
        }
    }
}
