using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public interface IGameStateService
    {
        IUniverseCollection UniverseCollection { get; }
        IWorldCollection WorldCollection { get; }
        IAreaCollection AreaCollection{ get; }
        ISectionCollection SectionCollection { get; }
        IPlayerCollection PlayerCollection { get; }
        void AddPlayerToTheGame(LoginData data, string connectionId);
        void MovePlayerToArea(string playerId, string areaId);
    }
}
