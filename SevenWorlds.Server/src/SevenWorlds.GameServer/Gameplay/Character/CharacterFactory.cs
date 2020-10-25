using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Factories
{
    public class CharacterFactory : DataFactory, ICharacterFactory
    {
        private readonly ILogService logService;
        private readonly ISkillFactory skillFactory;

        public CharacterFactory(ILogService logService, ISkillFactory skillFactory)
        {
            this.logService = logService;
            this.skillFactory = skillFactory;
        }

        public CharacterData NewCharacter(string playerName, string worldId, CharacterType characterType)
        {
            logService.Log($"Creating new character for: {playerName} on world: {worldId}");

            // General
            var characterData = new CharacterData(playerName, characterType);
            characterData.Id = GetGUID();
            characterData.WorldId = worldId;
            characterData.Level = 1;
            SetDefaultValues(characterData);


            // Skills
            characterData.Skills = GetInitialMethods(characterType);

            // Resources
            characterData.Resources = new WorldResourcesData();

            // Refresh
            RefreshCharacter(characterData);

            return characterData;
        }

        private static List<SkillType> GetInitialMethods(CharacterType type)
        {
            switch (type) {
                default:
                    return new List<SkillType>() {
                        SkillType.WeaponAttack
                    };
            }
            
        }

        public void RefreshCharacter(CharacterData data)
        {
            // Refresh initial combat data
            data.CombatData = new CombatData(data.Id, skillFactory.GetListOfSkillDatas(
                data.Skills
            ));

            data.CombatData.AddEquipData(data.Equipments);

            
        }
    }
}
