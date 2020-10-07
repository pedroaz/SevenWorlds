using SevenWorlds.GameServer.Gameplay.Player.Actions;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerActionFactory : IPlayerActionFactory
    {
        public PlayerAction GenerateAction(PlayerActionData data)
        {
            switch (data.ActionType) {
                case PlayerActionType.Movement:
                    return new PlayerMovementAction(data);
                case PlayerActionType.Attack:
                    return new PlayerAttackAction(data);
                default:
                    return new PlayerAction(data);
            }
        }
    }
}
