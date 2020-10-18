using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Base
{
    public class PlayerActionCollection
    {
        public Queue<MovementActionData> Movement { get; set; }
        public Queue<StartBattleActionData> StartBattle { get; set; }

        public PlayerActionCollection()
        {
            Movement = new Queue<MovementActionData>();
            StartBattle = new Queue<StartBattleActionData>();
        }
    }
}
