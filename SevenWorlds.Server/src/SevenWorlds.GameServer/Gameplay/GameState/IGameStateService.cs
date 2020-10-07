using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public interface IGameStateService
    {
        IUniverseCollection UniverseCollection { get; }
        IWorldCollection WorldCollection { get; }
        IAreaCollection AreaCollection { get; }
        ISectionCollection SectionCollection { get; }
        IPlayerCollection PlayerCollection { get; }
        PlayerData AddPlayerToTheGame(LoginData data, string connectionId);
        void MovePlayerToArea(string playerId, string areaId);
        UniverseSyncData GetUniverseSyncData();
        WorldSyncData GetWorldSyncData(string worldId);
        AreaSyncData GetAreaSyncData(string areaId);
    }
}
