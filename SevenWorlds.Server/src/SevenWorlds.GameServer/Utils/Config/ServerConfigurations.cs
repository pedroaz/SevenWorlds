using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Config
{
    public class ServerConfigurations
    {
        public bool AutoStart { get; set; }
        public string ServerId { get; set; }
        public string MongoDbKey { get; set; }
        public string LogFilePath { get; set; }
        public string MasterDataDumpFolder { get; set; }
        public string MonsterStoragePath { get; set; }
        public string SkillStoragePath { get; set; }
        public string EquipmentsStoragePath { get; set; }
        public string TalentsStoragePath { get; set; }
    }
}
