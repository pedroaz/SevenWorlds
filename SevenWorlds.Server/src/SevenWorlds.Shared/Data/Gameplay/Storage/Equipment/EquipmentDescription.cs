﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Storage.Equipment
{
    public class EquipmentDescription
    {
        public int BaseCD;

        public string EquipmentName;
        public EquipmentId EquipmentId;
        public EquipmentType Type;
        public int MaxHp;

        public int Attack;
        public int Defense;
        public int Speed;

        public int Fire;
        public int Water;
        public int Earth;
        public int Air;
    }
}
