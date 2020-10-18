using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.Loop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Executor
{
    public interface IPlayerActionExecutor
    {
        void SetSyncCoordinator(SyncCoordinator syncCoordinator);
        void SetActionCollection(PlayerActionBundle bundle);
        void ExecuteMovementActions();
        void ExecuteStartBattleActions();
    }
}
