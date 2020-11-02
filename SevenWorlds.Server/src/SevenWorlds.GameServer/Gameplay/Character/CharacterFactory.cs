using Newtonsoft.Json;
using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.Talent;
using SevenWorlds.GameServer.Utils.Config;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public class CharacterFactory : DataFactory, ICharacterFactory
    {
        private readonly ILogService logService;
        private readonly ISkillFactory skillFactory;
        private readonly IDatabaseService databaseService;
        private readonly IConfigurator configurator;
        private readonly ITalentFactory talentFactory;
        private Dictionary<CharacterType, CharacterDescription> storage = new Dictionary<CharacterType, CharacterDescription>();

        public CharacterFactory(ILogService logService, ISkillFactory skillFactory, 
            IDatabaseService databaseService, IConfigurator configurator, ITalentFactory talentFactory)
        {
            this.logService = logService;
            this.skillFactory = skillFactory;
            this.databaseService = databaseService;
            this.configurator = configurator;
            this.talentFactory = talentFactory;
        }

        public async Task<CharacterData> NewCharacter(string playerName, string worldId, CharacterType characterType)
        {
            logService.Log($"Creating new character for: {playerName} on world: {worldId}");

            // Create
            var characterDescription = storage[characterType];
            var characterData = new CharacterData(playerName, characterDescription);

            // General
            SetDefaultValues(characterData);
            characterData.WorldId = worldId;
            characterData.Level = 1;

            // Skills
            characterData.Skills = new List<SkillType>();

            // Resources
            characterData.Resources = new WorldResourcesData();

            // Talents
            characterData.TalentBundle = talentFactory.CreateNewBundle(characterDescription);

            // Equipments
            characterData.Equipments = new EquipmentBundle();

            // Refresh
            RefreshCharacter(characterData);

            // Save to DB
            await databaseService.InsertCharacter(characterData);

            logService.Log("Finished creating character");

            return characterData;
        }

        public void RefreshCharacter(CharacterData characterData)
        {
            // Refresh initial combat data
            characterData.CombatData = new CombatData(characterData.Id, skillFactory.GetListOfSkillDatas(
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
