using SevenWorlds.GameServer.Utils.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfigurator configurator;

        public DatabaseService(IConfigurator configurator)
        {
            this.configurator = configurator;
        }
    }
}
