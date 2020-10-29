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
        public ServerConfigurations Config { get; private set; }

        public void ReadConfigurations(string configFilePath)
        {
            Config = JsonConvert.DeserializeObject<ServerConfigurations>(File.ReadAllText(configFilePath));
            SetEnvFolder(configFilePath);
            SetStoragePaths();
            SetLogPath();
            SetMasterDataDumpPath();
        }

        private void SetEnvFolder(string configFilePath)
        {
            Config.EnvFolder = Path.GetDirectoryName(configFilePath);
        }

        private void SetMasterDataDumpPath()
        {
            Config.MasterDataDumpFolder = Path.Combine(Config.EnvFolder, "MasterDataDumps");
        }

        private void SetLogPath()
        {
            Config.LogFilePath = Path.Combine(Config.EnvFolder, "Logs", "ServerLog.txt");
        }

        private void SetStoragePaths()
        {
            Config.MonsterStoragePath = Path.Combine(Config.EnvFolder, "StoragesFolder", "MonsterStorage.json");
            Config.EquipmentsStoragePath = Path.Combine(Config.EnvFolder, "StoragesFolder", "EquipmentStorage.json");
            Config.SkillStoragePath = Path.Combine(Config.EnvFolder, "StoragesFolder", "SkillStorage.json");
            Config.TalentsStoragePath = Path.Combine(Config.EnvFolder, "StoragesFolder", "TalentStorage.json");
        }
    }
}
