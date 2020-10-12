﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.Log
{
    public interface ILogService
    {
        void Log(string message);
        void Log(Exception e);
        void Log(string message, LogDestination destination);
    }
}
