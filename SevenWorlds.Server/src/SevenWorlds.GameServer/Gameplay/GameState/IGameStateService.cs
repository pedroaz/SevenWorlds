using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Encounter;
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
        ICharacterCollection CharacterCollection { get; }
        IEncounterCollection EncounterCollection { get; }

        // Player
        void AddPlayerToGame(PlayerData playerData);

        // Characters 
        void AddCharacterToGame(CharacterData characterData);
        void MovePlayerToArea(string characterId, string areaId);
        void MovePlayerToArea(string characterId, WorldPosition areaPosition);


        // Syncs
        UniverseSyncData GetUniverseSyncData();
        WorldSyncData GetWorldSyncData(string worldId);
        AreaSyncData GetAreaSyncData(string areaId);
    }
}
