using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Loop
{
    public class SyncCoordinator
    {
        public List<string> AreasToSync { get; set; }

        public SyncCoordinator()
        {
            AreasToSync = new List<string>();
        }

        public void Clear()
        {
            AreasToSync.Clear();
        }
    }
}
