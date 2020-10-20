using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Encounter;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Gameplay.Section;
using SevenWorlds.GameServer.Gameplay.Universe;
using SevenWorlds.GameServer.Gameplay.World;
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
            IEncounterCollection encounterCollection,
            ILogService logService)
        {
            UniverseCollection = universeCollection;
            WorldCollection = worldCollection;
            AreaCollection = areaCollection;
            SectionCollection = sectionCollection;
            PlayerCollection = playerCollection;
            CharacterCollection = characterCollection;
            EncounterCollection = encounterCollection;
            this.logService = logService;
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

        public void MovePlayerToArea(string characterId, string areaId)
        {
            logService.Log($"Moving character on the game state: {characterId}");
            var character = CharacterCollection.FindById(characterId);
            var area = AreaCollection.FindById(areaId);
            character.Position = area.Position;
            character.AreaId = area.Id;
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
            model.Encounters = EncounterCollection.Bundle;
            return model;
        }
    }
}
