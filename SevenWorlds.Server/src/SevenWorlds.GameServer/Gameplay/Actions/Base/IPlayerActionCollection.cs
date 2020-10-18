using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public interface IPlayerActionCollection
    {
        void AddToBundle(MovementActionData action);
        void AddToBundle(StartBattleActionData action);
        PlayerActionBundle CopyActionCollection();
    }
}
