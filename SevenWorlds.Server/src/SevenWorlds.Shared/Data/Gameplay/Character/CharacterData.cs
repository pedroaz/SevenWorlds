using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;
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
        public EquipmentBundle Equipments;
        public TalentBundle TalentBundle;

        // Resources
        public WorldResourcesData Resources;

        public CharacterData(string playerName, CharacterDescription description)
        {
            PlayerName = playerName;
            if(description != null) {
                Type = description.CharacterType;
            }
        }

        public int GetAvailableTalentPoints()
        {
            return Level - TalentBundle.GetAmountOfSpentTalentPoints();
        }
    }
}
