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
        private ServerConfigurations config;

        public ServerConfigurations Config => config;

        public void ReadConfigurations(string configFilePath)
        {
            config = JsonConvert.DeserializeObject<ServerConfigurations>(File.ReadAllText(configFilePath));
        }
    }
}
