using Newtonsoft.Json;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Storage.Equipment;
using System.Collections.Generic;
using System.IO;

namespace SevenWorlds.GameServer.Gameplay.Equipment
{
    public class EquipmentFactory : IEquipmentFactory
    {
        private readonly IConfigurator configurator;

        private Dictionary<EquipmentId, EquipmentDescription> storage = new Dictionary<EquipmentId, EquipmentDescription>();

        public EquipmentFactory(IConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.EquipmentsStoragePath);
            storage = JsonConvert.DeserializeObject<Dictionary<EquipmentId, EquipmentDescription>>(json);
        }

        public EquipmentData CreateNewEquipment(EquipmentId id)
        {
            return new EquipmentData(storage[id]);
        }
    }
}
