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
        public string PlayerName;

        // General
        public int Level;
        public bool IsOnline;
        public string WorldId;

        // Position - Needs to be set by [CharacterPlacementService]
        public string AreaId;
        public WorldPosition Position;

        // Combat
        public CombatData InitialCombatData;

        // HP
        public HpData HpData;
        public List<SkillType> Skills;

        public EquipmentData Equipments;

        // Resources
        public CharacterResourcesData Resources { get; set; }

        public CharacterData(string playerName)
        {
            PlayerName = playerName;
        }

    }
}
