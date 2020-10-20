using SevenWorlds.Shared.Data.Factory;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Factories
{
    public class CharacterFactory : DataFactory
    {
        public CharacterData NewCharacter(string playerName, string worldId)
        {
            var data = new CharacterData(playerName);
            data.Id = GetGUID();
            data.WorldId = worldId;
            data.Level = 1;
            data.Combat = new CombatData();
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
    }
}
