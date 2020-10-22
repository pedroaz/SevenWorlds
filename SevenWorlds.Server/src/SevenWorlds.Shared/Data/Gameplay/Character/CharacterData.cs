using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class CharacterData : NetworkData
    {
        // Constant
        public string PlayerName { get; set; }

        // General
        public int Level { get; set; }
        public bool IsOnline { get; set; }
        public string WorldId { get; set; }

        // Position - Needs to be set by [CharacterPlacementService]
        public string AreaId { get; set; }
        public WorldPosition Position { get; set; }

        // Combat
        public CombatData InitialCombatData { get; set; }

        // HP
        public HpData HpData { get; set; }
        public List<SkillType> Skills { get; set; }

        public EquipmentData Equipments { get; set; }

        // Resources
        public CharacterResourcesData Resources { get; set; }

        public CharacterData(string playerName)
        {
            PlayerName = playerName;
        }

    }
}
