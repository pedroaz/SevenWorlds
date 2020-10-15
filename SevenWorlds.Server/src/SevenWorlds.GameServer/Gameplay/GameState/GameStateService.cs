using SevenWorlds.GameServer.Gameplay.Area;
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

        public GameStateService(
            IUniverseCollection universeCollection,
            IWorldCollection worldCollection,
            IAreaCollection areaCollection,
            ISectionCollection sectionCollection,
            IPlayerCollection playerCollection)
        {
            this.universeCollection = universeCollection;
            this.worldCollection = worldCollection;
            this.areaCollection = areaCollection;
            this.sectionCollection = sectionCollection;
            this.playerCollection = playerCollection;
        }

        public IUniverseCollection UniverseCollection => universeCollection;

        public IWorldCollection WorldCollection => worldCollection;

        public IAreaCollection AreaCollection => areaCollection;

        public ISectionCollection SectionCollection => sectionCollection;

        public IPlayerCollection PlayerCollection => playerCollection;

        public void AddPlayerDataToGame(PlayerData playerData)
        {
            playerCollection.Add(playerData);
        }

        public void AddCharacterToWorld(string playerId, string connectionId)
        {

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
