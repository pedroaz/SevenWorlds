﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Config
{
    public interface IConfigurator
    {
        void ReadConfigurations(string configFilePath);
        bool ShouldAutoStart();
        string GetServerId();
        string GetMongoDbKey();
        string GetLogFilePath();
        string GetMasterDataDumpFoler();
    }
}