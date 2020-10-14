using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.Universe
{
    public class UniverseCollection : IUniverseCollection
    {
        private List<UniverseData> universes;
        public ILogService logService { get; }

        public UniverseCollection(ILogService logService)
        {
            universes = new List<UniverseData>();
            this.logService = logService;
        }

        public void Add(UniverseData data)
        {
            universes.Add(data);
        }

        public UniverseData FindById(string id)
        {
            return universes.Find(x => x.ObjectId == id);
        }

        public void Remove(string id)
        {
            universes.RemoveAll(x => x.ObjectId == id);
        }

        public void StartUniverses()
        {
            logService.Log("Setting up universes with fake data");
        }

        public IEnumerable<UniverseData> GetAll()
        {
            return universes;
        }

        public UniverseData GetDefaultUniverse()
        {
            return universes.FirstOrDefault();
        }
    }
}
