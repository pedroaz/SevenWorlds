using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum CharacterType
    {
        Warrior,
        ElementalWarrior,
        Merchant,
        Mage,
        DarkMage,
        Explorer,
        Hunter,
        Assassin,
        BloodWarrior
    }

    [System.Serializable]
    public class CharacterData : NetworkData
    {
        // Constant
        public string PlayerName;
        public CharacterType Type;

        // General
        public int Level;
        public string WorldId;

        // Position - Needs to be set by [CharacterPlacementService]
        public string AreaId;
        public Position Position;

        // Combat
        public CombatData CombatData;

        public List<SkillType> Skills;
        public EquipmentData Equipments;

        // Resources
        public WorldResourcesData Resources;

        public CharacterData(string playerName, CharacterType type)
        {
            PlayerName = playerName;
            Type = type;
        }

    }
}
