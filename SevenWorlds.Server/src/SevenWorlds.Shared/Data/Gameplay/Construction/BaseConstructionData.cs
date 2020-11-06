using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Construction.Base
{
    public class BaseConstructionData
    {
        public string Id;
        public DateTime DateOfCreation;

        public void SetupDefaultValues()
        {
            Id = Guid.NewGuid().ToString();
            DateOfCreation = DateTime.Now;
        }
    }
}
