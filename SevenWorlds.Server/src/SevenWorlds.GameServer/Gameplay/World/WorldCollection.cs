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
            return worlds.Find(x => x.Id == id);
        }

        public void Remove(string id)
        {
            worlds.RemoveAll(x => x.Id == id);
        }

        public List<WorldData> GetAll()
        {
            return worlds;
        }
    }
}
