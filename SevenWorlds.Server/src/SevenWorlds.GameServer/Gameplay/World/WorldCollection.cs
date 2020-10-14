using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.World
{
    public class WorldCollection : IWorldCollection
    {
        private List<WorldData> worlds;
        ILogService logService { get; }

        public WorldCollection(ILogService logService)
        {
            worlds = new List<WorldData>();
            this.logService = logService;
        }

        public void Add(WorldData data)
        {
            worlds.Add(data);
        }

        public WorldData FindById(string id)
        {
            return worlds.Find(x => x.ObjectId == id);
        }

        public void Remove(string id)
        {
            worlds.RemoveAll(x => x.ObjectId == id);
        }

        public IEnumerable<WorldData> GetAll()
        {
            return worlds;
        }

        public List<WorldData> GetDefaultWorlds()
        {
            return worlds;
        }
    }
}
