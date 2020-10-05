using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Area
{
    public class AreaCollection : IAreaCollection
    {
        private List<AreaData> areas;
        private ILogService logService;

        public AreaCollection(ILogService logService)
        {
            areas = new List<AreaData>();
            this.logService = logService;
        }

        public void Add(AreaData data)
        {
            areas.Add(data);
        }

        public AreaData FindById(int id)
        {
            return areas.Find(x => x.Id == id);
        }

        public void Remove(int id)
        {
            areas.RemoveAll(x => x.Id == id);
        }
    }
}
