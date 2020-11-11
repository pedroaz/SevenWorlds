using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Base
{
    [System.Serializable]
    public class PlayerActionBundle
    {
        public List<MovementActionData> Movement { get; set; }

        public PlayerActionBundle()
        {
            Movement = new List<MovementActionData>();
        }
    }
}
