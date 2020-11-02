﻿using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.Battle.Factories;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System;
using System.Collections.Generic;
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

        public CharacterFactory(ILogService logService, ISkillFactory skillFactory, IDatabaseService databaseService)
        {
            this.logService = logService;
            this.skillFactory = skillFactory;
            this.databaseService = databaseService;
        }

        public async Task<bool> NewCharacter(string playerName, string worldId, CharacterType characterType)
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

            characterData.TalentBundle = new TalentBundle();

            // Refresh
            RefreshCharacter(characterData);

            await databaseService.InsertCharacter(playerName, characterData);

            return true;
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

        public void RefreshCharacter(CharacterData characterData)
        {
            // Refresh initial combat data
            characterData.CombatData = new CombatData(characterData.Id, skillFactory.GetListOfSkillDatas(
                characterData.Skills
            ));


            characterData.CombatData.AddEquipmentBundle(characterData.Equipments);

            foreach (var talent in characterData?.TalentBundle?.AvailableTalents) {
                if (talent.IsEnabled) {
                    talent.ApplyTalent(characterData);
                }
            }
        }
    }
}
