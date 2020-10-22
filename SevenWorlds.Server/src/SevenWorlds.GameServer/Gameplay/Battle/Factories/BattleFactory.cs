using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Factories
{
    public class BattleFactory : DataFactory, IBattleFactory
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IMonsterDataFactory monsterDataFactory;

        public BattleFactory(ILogService logService, IGameStateService gameStateService, IMonsterDataFactory monsterDataFactory)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.monsterDataFactory = monsterDataFactory;
        }

        public BattleData CreateNewBattle(StartBattleActionData startData)
        {
            BattleData battleData = new BattleData(startData);

            // Characters
            battleData.CharacterDatas = new List<Gameplay.CharacterData>();
            foreach (var characterId in startData.InitialCharacters) {
                var characterData = gameStateService.CharacterCollection.FindById(characterId);
                battleData.CharacterDatas.Add(characterData);
                battleData.CharactersCombatData.Add(characterData.InitialCombatData.Copy());
            }

            // Monsters
            battleData.MonsterDatas = new List<MonsterData>();
            foreach (var monsterType in startData.Monsters) {
                battleData.MonsterDatas.Add(monsterDataFactory.GetMonsterData(monsterType));
            }

            SetDefaultValues(battleData);
            return battleData;
        }
    }
}
