﻿using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
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
        void AddPlayerDataToGame(PlayerData playerData);
        void MovePlayerToArea(string playerId, string areaId);
        UniverseSyncData GetUniverseSyncData();
        WorldSyncData GetWorldSyncData(string worldId);
        AreaSyncData GetAreaSyncData(string areaId);
    }
}
