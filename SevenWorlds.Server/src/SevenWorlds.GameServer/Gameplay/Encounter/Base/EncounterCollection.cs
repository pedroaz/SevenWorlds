using SevenWorlds.GameServer.Gameplay.Encounter.Base;
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
        private List<EncounterInstance> encounters = new List<EncounterInstance>();

        public void Add(EncounterInstance data)
        {
            encounters.Add(data);
        }

        public EncounterInstance FindById(string id)
        {
            return encounters.Find(x => x.Id == id);
        }

        public IEnumerable<EncounterInstance> GetAll()
        {
            return encounters;
        }

        public void Remove(string id)
        {
            encounters.RemoveAll(x => x.Id == id);
        }
    }
}
