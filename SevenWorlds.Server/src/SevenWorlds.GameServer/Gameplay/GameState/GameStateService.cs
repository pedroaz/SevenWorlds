using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
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
        private readonly IUniverseCollection universeCollection;
        private readonly IWorldCollection worldCollection;
        private readonly IAreaCollection areaCollection;
        private readonly ISectionCollection sectionCollection;
        private readonly IPlayerCollection playerCollection;
        private readonly ICharacterCollection characterCollection;

        public GameStateService(
            IUniverseCollection universeCollection,
            IWorldCollection worldCollection,
            IAreaCollection areaCollection,
            ISectionCollection sectionCollection,
            IPlayerCollection playerCollection,
            ICharacterCollection characterCollection)
        {
            this.universeCollection = universeCollection;
            this.worldCollection = worldCollection;
            this.areaCollection = areaCollection;
            this.sectionCollection = sectionCollection;
            this.playerCollection = playerCollection;
            this.characterCollection = characterCollection;
        }

        public IUniverseCollection UniverseCollection => universeCollection;

        public IWorldCollection WorldCollection => worldCollection;

        public IAreaCollection AreaCollection => areaCollection;

        public ISectionCollection SectionCollection => sectionCollection;

        public IPlayerCollection PlayerCollection => playerCollection;

        public ICharacterCollection CharacterCollection => characterCollection;

        public void AddPlayerToGame(PlayerData playerData)
        {
            playerCollection.Add(playerData);
        }

        public void AddCharacterToGame(CharacterData characterData)
        {
            characterCollection.Add(characterData);
        }

        public void MovePlayerToArea(string playerId, string areaId)
        {
        }

        public AreaSyncData GetAreaSyncData(string areaId)
        {
            return new AreaSyncData() {

            };
        }

        public UniverseSyncData GetUniverseSyncData()
        {
            return new UniverseSyncData() {
                Universe = universeCollection.GetDefaultUniverse(),
                Worlds = worldCollection.GetAll().ToList()
            };
        }

        public WorldSyncData GetWorldSyncData(string worldId)
        {
            return new WorldSyncData() {
                World = worldCollection.FindById(worldId),
                Areas = areaCollection.GetAllAreasFromWorld(worldId)
            };
        }

        public IEnumerable<WorldData> GetAllWorlds()
        {
            return worldCollection.GetAll();
        }

        public IEnumerable<AreaData> GetAllAreas()
        {
            return areaCollection.GetAll();
        }

       
    }
}
