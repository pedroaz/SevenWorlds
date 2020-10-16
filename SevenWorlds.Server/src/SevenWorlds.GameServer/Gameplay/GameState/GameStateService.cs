using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Encounter;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System.Collections.Generic;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public class GameStateService : IGameStateService
    {
        public IUniverseCollection UniverseCollection { get; }
        public IWorldCollection WorldCollection { get; }
        public IAreaCollection AreaCollection { get; }
        public ISectionCollection SectionCollection { get; }
        public IPlayerCollection PlayerCollection { get; }
        public ICharacterCollection CharacterCollection { get; }
        public IEncounterCollection EncounterCollection { get;  }

        public GameStateService(
            IUniverseCollection universeCollection,
            IWorldCollection worldCollection,
            IAreaCollection areaCollection,
            ISectionCollection sectionCollection,
            IPlayerCollection playerCollection,
            ICharacterCollection characterCollection,
            IEncounterCollection encounterCollection)
        {
            UniverseCollection = universeCollection;
            WorldCollection = worldCollection;
            AreaCollection = areaCollection;
            SectionCollection = sectionCollection;
            PlayerCollection = playerCollection;
            CharacterCollection = characterCollection;
            EncounterCollection = encounterCollection;
        }

        

        public void AddPlayerToGame(PlayerData playerData)
        {
            PlayerCollection.Add(playerData);
        }

        public void AddCharacterToGame(CharacterData characterData)
        {
            CharacterCollection.Add(characterData);
        }

        public void MovePlayerToArea(string characterId, string areaId)
        {
            CharacterCollection.FindById(characterId).Position = AreaCollection.FindById(areaId).Position;
        }

        public AreaSyncData GetAreaSyncData(string areaId)
        {
            return new AreaSyncData() {
                
            };
        }

        public UniverseSyncData GetUniverseSyncData()
        {
            return new UniverseSyncData() {
                Universe = UniverseCollection.GetDefaultUniverse(),
                Worlds = WorldCollection.GetAll().ToList()
            };
        }

        public WorldSyncData GetWorldSyncData(string worldId)
        {
            return new WorldSyncData() {
                World = WorldCollection.FindById(worldId),
                Areas = AreaCollection.GetAllAreasFromWorld(worldId)
            };
        }

        public IEnumerable<WorldData> GetAllWorlds()
        {
            return WorldCollection.GetAll();
        }

        public IEnumerable<AreaData> GetAllAreas()
        {
            return AreaCollection.GetAll();
        }

       
    }
}
