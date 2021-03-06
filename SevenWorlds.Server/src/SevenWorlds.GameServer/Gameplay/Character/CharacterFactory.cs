﻿using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Talent;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly ILogService logService;
        private readonly ISkillFactory skillFactory;
        private readonly IDatabaseService databaseService;
        private readonly IConfigurator configurator;
        private readonly ITalentFactory talentFactory;
        private readonly ICharacterPlacementService characterPlacementService;
        private readonly IGameStateService gameStateService;
        private Dictionary<CharacterType, CharacterDescription> storage = new Dictionary<CharacterType, CharacterDescription>();

        public CharacterFactory(ILogService logService, ISkillFactory skillFactory,
            IDatabaseService databaseService, IConfigurator configurator, ITalentFactory talentFactory, 
            ICharacterPlacementService characterPlacementService, IGameStateService gameStateService)
        {
            this.logService = logService;
            this.skillFactory = skillFactory;
            this.databaseService = databaseService;
            this.configurator = configurator;
            this.talentFactory = talentFactory;
            this.characterPlacementService = characterPlacementService;
            this.gameStateService = gameStateService;
        }

        public async Task<CharacterData> NewCharacter(string playerName, string worldId, CharacterType characterType)
        {
            logService.Log($"Creating new character for: {playerName} on world: {worldId}");

            // Create
            var characterDescription = storage[characterType];
            var characterData = new CharacterData(playerName, characterDescription);

            // General
            characterData.WorldId = worldId;
            characterData.CombatData.Level = 1;
            characterData.CombatData.UnitName = playerName;
            characterData.Id = Guid.NewGuid().ToString();

            // Skills
            characterData.Skills = new List<SkillId>();

            // Resources
            characterData.Resources = new WorldResourcesData();

            // Talents
            characterData.TalentBundle = talentFactory.CreateNewBundle(characterDescription);

            // Equipments
            characterData.Equipments = new EquipmentBundle();

            // Refresh
            RefreshCharacter(characterData);

            // Add to game
            gameStateService.AddCharacterToGame(characterData);
            characterPlacementService.PlaceCharacterOnCity(characterData.Id);


            // Save to DB
            await databaseService.InsertCharacter(characterData);

            logService.Log("Finished creating character");

            return characterData;
        }

        public void RefreshCharacter(CharacterData characterData)
        {
            // Refresh initial combat data
            characterData.CombatData = new CombatData(characterData.Id, skillFactory.CreateListOfSkillDatas(
                characterData.Skills
            ));


            characterData.CombatData.AddStatsFromAllEquipments(characterData.Equipments);

            foreach (var row in characterData.TalentBundle.TalentRows) {

                foreach (TalentData talentData in row) {
                    talentData.ApplyTalent(characterData);
                }
            }

        }

        public void SetupStorage()
        {
            var json = File.ReadAllText(configurator.Config.CharacterStorage);
            storage = JsonConvert.DeserializeObject<Dictionary<CharacterType, CharacterDescription>>(json);
        }
    }
}
