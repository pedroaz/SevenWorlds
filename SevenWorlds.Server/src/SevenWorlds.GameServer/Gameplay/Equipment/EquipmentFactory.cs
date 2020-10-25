using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Equipment
{
    public class EquipmentFactory : IEquipmentFactory
    {
        private readonly IConfigurator configurator;

        private Dictionary<EquipmentType, EquipmentData> storage = new Dictionary<EquipmentType, EquipmentData>();

        public EquipmentFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.EquipmentsStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<EquipmentType, EquipmentData>>(json);
        }
    }
}
