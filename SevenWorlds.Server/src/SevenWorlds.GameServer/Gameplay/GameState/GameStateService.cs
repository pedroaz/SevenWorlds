using SevenWorlds.GameServer.Database.Models;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Battle.Base;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Sync;
using System.Collections.Generic;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.GameState
{
    public class GameStateService : IGameStateService
    {
        private readonly ILogService logService;
        private readonly IHubService hubService;

        public IUniverseCollection UniverseCollection { get; }
        public IWorldCollection WorldCollection { get; }
        public IAreaCollection AreaCollection { get; }
        public ISectionCollection SectionCollection { get; }
        public IPlayerCollection PlayerCollection { get; }
        public ICharacterCollection CharacterCollection { get; }
        public IBattleCollection BattleCollection { get; }

        public GameStateService(
            IUniverseCollection universeCollection,
            IWorldCollection worldCollection,
            IAreaCollection areaCollection,
            ISectionCollection sectionCollection,
            IPlayerCollection playerCollection,
            ICharacterCollection characterCollection,
            IBattleCollection battleCollection,
            ILogService logService,
            IHubService hubService)
        {
            UniverseCollection = universeCollection;
            WorldCollection = worldCollection;
            AreaCollection = areaCollection;
            SectionCollection = sectionCollection;
            PlayerCollection = playerCollection;
            CharacterCollection = characterCollection;
            BattleCollection = battleCollection;

            this.logService = logService;
            this.hubService = hubService;
        }



        public void AddPlayerToGame(PlayerData playerData)
        {
            logService.Log($"Adding player to game state: {playerData.PlayerName}");
            PlayerCollection.Add(playerData);
        }

        public void AddCharacterToGame(CharacterData characterData)
        {
            logService.Log($"Adding character to game state: {characterData.Id}");
            CharacterCollection.Add(characterData);
        }

        public void MovePlayerToArea(string characterId, string destinationAreaId)
        {
            logService.Log($"Moving character on the game state: {characterId}");
            var character = CharacterCollection.FindById(characterId);
            var player = PlayerCollection.FindByPlayerName(character.PlayerName);
            hubService.AddPlayerToAreaGroup(player.ConnectionId, character?.AreaId);
            var area = AreaCollection.FindById(destinationAreaId);
            hubService.AddPlayerToAreaGroup(player.ConnectionId, destinationAreaId);
            character.Position = area.Position;
            character.AreaId = area.Id;
            character.movementStatus = CharacterMovementStatus.InPlace;
        }

        public AreaSyncData GetAreaSyncData(string areaId)
        {
            return new AreaSyncData() {
                Area = AreaCollection.FindById(areaId),
                Characters = CharacterCollection.FindCharacterOnArea(areaId),
                Sections = SectionCollection.FindAllSectionsByArea(areaId)
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

        public List<WorldData> GetAllWorlds()
        {
            return WorldCollection.GetAll();
        }

        public List<AreaData> GetAllAreas()
        {
            return AreaCollection.GetAll();
        }

        public MasterDataModel GetMasterData()
        {
            MasterDataModel model = new MasterDataModel();
            model.Universes = UniverseCollection.GetAll();
            model.Worlds = WorldCollection.GetAll();
            model.Areas = AreaCollection.GetAll();
            model.Sections = SectionCollection.Bundle;
            return model;
        }

        public void RemovePlayerFromTheGame(string connectionId)
        {
            logService.Log($"Tring to remove player with connection id from the server {connectionId}");
            var playerData = PlayerCollection.FindByConnectionId(connectionId);

            if (playerData == null) {
                logService.Log($"Wasn't able to find any player with connection id {connectionId}");
                return;
            }

            PlayerCollection.RemovePlayer(playerData.PlayerName);
            CharacterCollection.RemovePlayerCharacters(playerData.PlayerName);
        }
    }
}
