using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Loop
{
    public class LoopSyncCoordinator
    {
        public List<string> AreasToSync { get; set; }

        public LoopSyncCoordinator()
        {
            AreasToSync = new List<string>();
        }

        public void Clear()
        {
            AreasToSync.Clear();
        }
    }
}
