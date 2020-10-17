using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Encounter
{
    public class EncounterCollection : IEncounterCollection
    {
        private List<EncounterData> encounters = new List<EncounterData>();

        public void Add(EncounterData data)
        {
            encounters.Add(data);
        }

        public EncounterData FindById(string id)
        {
            return encounters.Find(x => x.Id == id);
        }

        public IEnumerable<EncounterData> GetAll()
        {
            return encounters;
        }

        public void Remove(string id)
        {
            encounters.RemoveAll(x => x.Id == id);
        }
    }
}
