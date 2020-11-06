using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public class BattleFactory : IBattleFactory
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
            foreach (var characterId in startData.InitialCharacters) {
                var characterData = gameStateService.CharacterCollection.FindById(characterId);
                battleData.Characters.Add(characterData);
            }

            // Monsters
            foreach (var monsterType in startData.Monsters) {
                MonsterData monsterData = monsterDataFactory.GetMonsterData(monsterType, 1);
                battleData.Monsters.Add(monsterData);
            }

            return battleData;
        }
    }
}
