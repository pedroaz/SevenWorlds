using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Actions.Base
{
    public interface IPlayerActionCollection
    {
        void AddToBundle(MovementActionData action);
        PlayerActionBundle CopyActionCollection();
    }
}
