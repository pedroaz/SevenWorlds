using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factories;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Actions.Executor
{
    public class PlayerActionExecutor : IPlayerActionExecutor
    {
        private readonly IGameStateService gameStateService;
        private readonly ILogService logService;
        private readonly IMonsterDataFactory monsterDataFactory;
        private SyncCoordinator syncCoordinator;
        private PlayerActionBundle actionBundle;
        private BattleFactory battleFactory = new BattleFactory();

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
            foreach (StartBattleActionData startData in actionBundle.StartBattle) {
                var data = battleFactory.CreateNewBattle(startData);

                // data.Monsters = 

                // data.




                gameStateService.BattleCollection.Add(data);
            }
        }
    }
}
