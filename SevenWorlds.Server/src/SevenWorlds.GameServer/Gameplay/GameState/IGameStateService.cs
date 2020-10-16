using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System.Collections;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public interface IGameStateService
    {
        IUniverseCollection UniverseCollection { get; }
        IWorldCollection WorldCollection { get; }
        IAreaCollection AreaCollection { get; }
        ISectionCollection SectionCollection { get; }
        IPlayerCollection PlayerCollection { get; }
        ICharacterCollection CharacterCollection { get; }

        // Player
        void AddPlayerToGame(PlayerData playerData);

        // Characters 
        void AddCharacterToGame(CharacterData characterData);
        void MovePlayerToArea(string playerId, string areaId);


        // Syncs
        UniverseSyncData GetUniverseSyncData();
        WorldSyncData GetWorldSyncData(string worldId);
        AreaSyncData GetAreaSyncData(string areaId);
    }
}
