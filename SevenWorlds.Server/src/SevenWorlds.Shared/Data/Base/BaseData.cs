﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Base
{
    public class BaseData
    {
        public readonly string Id = Guid.NewGuid().ToString();
    }
}
