using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using System;

namespace SevenWorlds.GameServer.Gameplay.Actions.Executor
{
    public class PlayerActionExecutor : IPlayerActionExecutor
    {
        private readonly IGameStateService gameStateService;
        private readonly ILogService logService;
        private readonly IMonsterDataFactory monsterDataFactory;
        private SyncCoordinator syncCoordinator;
        private PlayerActionBundle actionBundle;

        public PlayerActionExecutor(IGameStateService gameStateService, ILogService logService, IMonsterDataFactory monsterDataFactory)
        {
            this.gameStateService = gameStateService;
            this.logService = logService;
            this.monsterDataFactory = monsterDataFactory;
        }

        public void SetSyncCoordinator(SyncCoordinator syncCoordinator)
        {
            this.syncCoordinator = syncCoordinator;
        }

        public void SetActionCollection(PlayerActionBundle bundle)
        {
            this.actionBundle = bundle;
        }


        public void ExecuteMovementActions()
        {
            foreach (var data in actionBundle.Movement) {

                logService.Log($"Executing movement action from character: {data.CharacterId}");
                gameStateService.MovePlayerToArea(data.CharacterId, data.ToAreaId);
                syncCoordinator.AreasToSync.Add(data.FromAreaId);
                syncCoordinator.AreasToSync.Add(data.ToAreaId);
            }
        }

        public void ExecuteStartBattleActions()
        {
            
        }
    }
}
