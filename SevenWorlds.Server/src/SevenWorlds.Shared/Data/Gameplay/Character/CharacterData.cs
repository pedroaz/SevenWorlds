using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using SevenWorlds.Shared.Data.Gameplay.Talent;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum CharacterType
    {
        None,
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

    public enum CharacterMovementStatus
    {
        InPlace,
        Moving
    }

    [System.Serializable]
    public class CharacterData
    {

        // Constant
        public string PlayerName;
        public CharacterType Type;
        public string Id;

        // General
        public string WorldId;

        // Status
        public CharacterMovementStatus movementStatus;

        // Position - Needs to be set by [CharacterPlacementService]
        public string AreaId;
        public string AreaName;
        public Position CharacterAreaPosition;

        // Combat
        public CombatData CombatData;
        public List<SkillId> Skills;
        public EquipmentBundle Equipments;
        public TalentBundle TalentBundle;

        // Resources
        public WorldResourcesData Resources;

        public CharacterData(string playerName, CharacterDescription description)
        {
            PlayerName = playerName;
            if (description != null) {
                Type = description.CharacterType;
            }
        }

        public int GetAvailableTalentPoints()
        {
            return CombatData.Level - TalentBundle.GetAmountOfSpentTalentPoints();
        }
    }
}
