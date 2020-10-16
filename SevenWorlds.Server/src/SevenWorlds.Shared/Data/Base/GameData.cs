﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Base
{
    public class GameData
    {
        public string Id { get; set; }

        public static string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
