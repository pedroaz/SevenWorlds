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
        private readonly IGameStateService gameStateService;

        public CharacterPlacementService(IGameStateService gameStateService)
        {
            this.gameStateService = gameStateService;
        }

        public void PlaceAllPlayerCharactersIntoTheGame(string playerName)
        {
            foreach (var character in gameStateService.CharacterCollection.GetAll()) {
                character.IsOnline = true;
            }
        }
    }
}
