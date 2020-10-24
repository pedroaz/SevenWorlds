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

        public CharacterData NewCharacter(string playerName, string worldId)
        {
            logService.Log($"Creating new character for: {playerName} on world: {worldId}");

            var data = new CharacterData(playerName);
            data.Id = GetGUID();
            data.WorldId = worldId;
            data.Level = 1;

            List<SkillType> initialSkills = new List<SkillType>() {
                SkillType.WeaponAttack
            };

            data.Skills = initialSkills;
            data.CombatData = new CombatData(data.Id, skillFactory.GetListOfSkillDatas(
                initialSkills
            ));
            data.IsOnline = false;
            data.Resources = new CharacterResourcesData(){ 
                Resources = new Dictionary<string, int>() {
                    { CharacterResourceType.Gold.ToString(), 0 },
                    { CharacterResourceType.Rock.ToString(), 0 },
                    { CharacterResourceType.Wood.ToString(), 0 },
                }
            };
            SetDefaultValues(data);
            return data;
        }

        public void RefreshCharacter(CharacterData data)
        {
            // Refresh initial combat data
            data.CombatData = new CombatData(data.Id, skillFactory.GetListOfSkillDatas(
                data.Skills
            ));
        }
    }
}
