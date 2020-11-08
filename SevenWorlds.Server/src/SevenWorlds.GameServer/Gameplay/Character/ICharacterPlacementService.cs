using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public interface ICharacterPlacementService
    {
        void PlaceAllPlayerCharactersIntoTheGame(string playerName);
        void PlaceCharacterOnCity(string characterId);
    }
}
