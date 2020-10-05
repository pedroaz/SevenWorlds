using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public class GameStateService : IGameStateService
    {
        private readonly IUniverseCollection universeCollection;
        private readonly IWorldCollection worldCollection;
        private readonly IAreaCollection areaCollection;
        private readonly ISectionCollection sectionCollection;
        private readonly IPlayerCollection playerCollection;
        private readonly IHubService hubService;

        public GameStateService(
            IUniverseCollection universeCollection,
            IWorldCollection worldCollection,
            IAreaCollection areaCollection,
            ISectionCollection sectionCollection,
            IPlayerCollection playerCollection,
            IHubService hubService)
        {
            this.universeCollection = universeCollection;
            this.worldCollection = worldCollection;
            this.areaCollection = areaCollection;
            this.sectionCollection = sectionCollection;
            this.playerCollection = playerCollection;
            this.hubService = hubService;
        }

        public IUniverseCollection UniverseCollection => universeCollection;

        public IWorldCollection WorldCollection => worldCollection;

        public IAreaCollection AreaCollection => areaCollection;

        public ISectionCollection SectionCollection => sectionCollection;

        public IPlayerCollection PlayerCollection => playerCollection;

        public void AddPlayerToTheGame(LoginData data, string connectionId)
        {
            playerCollection.Add(new PlayerData() {
                Name = data.PlayerName,
                ConnectionId = connectionId
            });
        }

        public void MovePlayerToArea(string playerId, string areaId)
        {
            var player = playerCollection.FindById(playerId);
            var area = areaCollection.FindById(areaId);

            player.AreaId = areaId;

            if (player.AreaId != null) {
                hubService.RemovePlayerFromAreaGroup(player, area);
            }
            hubService.AddPlayerToAreaGroup(player, area);
            AreaSync(area);
        }

        private void AreaSync(AreaData area)
        {
            hubService.AreaSync(new AreaSyncData() {
                Area = area,
                Players = playerCollection.FindAllPlayersByArea(area.Id),
                Sections = sectionCollection.FindAllSectionsByArea(area.Id)
            });
        }
    }
}
