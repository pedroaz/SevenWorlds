using SevenWorlds.Shared.Data.Gameplay.Storage.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay.Equipment
{
    public class WeaponEquipmentData : EquipmentData
    {
        public int BaseCD;

        public WeaponEquipmentData(EquipmentDescription description) : base(description)
        {
            BaseCD = description.BaseCD;
        }
    }
}
