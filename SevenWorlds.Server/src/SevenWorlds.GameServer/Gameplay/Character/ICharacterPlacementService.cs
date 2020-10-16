using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public interface ICharacterPlacementService
    {
        Task PlaceAllPlayerCharactersIntoTheGame(string playerName);
    }
}
