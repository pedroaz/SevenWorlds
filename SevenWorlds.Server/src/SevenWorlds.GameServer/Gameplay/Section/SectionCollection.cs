using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Section
{
    public class SectionCollection : ISectionCollection
    {
        private List<SectionData> sections;
        private ILogService logService { get; }

        public SectionCollection(ILogService logService)
        {
            sections = new List<SectionData>();
            this.logService = logService;
        }

        public void Add(SectionData data)
        {
            sections.Add(data);
        }

        public SectionData FindById(int id)
        {
            return sections.Find(x => x.Id == id);
        }

        public void Remove(int id)
        {
            sections.RemoveAll(x => x.Id == id);
        }

        public IEnumerable<SectionData> GetAll()
        {
            return sections;
        }
    }
}
