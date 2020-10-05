using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

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

        public UniverseData FindById(int id)
        {
            return universes.Find(x => x.Id == id);
        }

        public void Remove(int id)
        {
            universes.RemoveAll(x => x.Id == id);
        }

        public void StartUniverses()
        {
            logService.Log("Setting up universes with fake data");
        }
    }
}
