using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public class CharacterPlacementService : ICharacterPlacementService
    {
        private readonly IDatabaseService databaseService;
        private readonly IGameStateService gameStateService;

        public CharacterPlacementService(IDatabaseService databaseService, IGameStateService gameStateService)
        {
            this.databaseService = databaseService;
            this.gameStateService = gameStateService;
        }

        public async Task PlaceAllPlayerCharactersIntoTheGame(string playerName)
        {
            var models = await databaseService.GetAllCharacterFromPlayer(playerName);
            foreach (var character in models) {
                gameStateService.AddCharacterToGame(character.data);
            }
        }
    }
}
