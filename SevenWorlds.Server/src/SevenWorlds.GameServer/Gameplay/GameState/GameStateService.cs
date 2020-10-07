using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
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

        public PlayerData AddPlayerToTheGame(LoginData data, string connectionId)
        {
            var playerData = new PlayerData() {
                Name = data.PlayerName,
                ConnectionId = connectionId,
                AreaId = areaCollection.GetAll().FirstOrDefault().Id
            };
            playerCollection.Add(playerData);
            return playerData;
        }

        public void MovePlayerToArea(string playerId, string areaId)
        {
            var player = playerCollection.FindById(playerId);

            player.AreaId = areaId;
        }

        public AreaSyncData GetAreaSyncData(string areaId)
        {
            return new AreaSyncData() {
                Area = areaCollection.FindById(areaId),
                Players = playerCollection.FindAllPlayersByArea(areaId),
                Sections = sectionCollection.FindAllSectionsByArea(areaId)
            };
        }

        public UniverseSyncData GetUniverseSyncData()
        {
            return new UniverseSyncData() {
                Universe = universeCollection.GetDefaultUniverse(),
                Worlds = worldCollection.GetDefaultWorlds()
            };
        }

        public WorldSyncData GetWorldSyncData(string worldId)
        {
            return new WorldSyncData() {
                World = worldCollection.FindById(worldId),
                Areas = areaCollection.GetAllAreasFromWorld(worldId)
            };
        }
    }
}
