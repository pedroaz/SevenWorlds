using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    [System.Serializable]
    public class CombatData
    {

        // General
        public int Level;
        public string UnitName;
        public Position BattlegroundPosition;

        // Hp
        public int MaxHp;
        public int CurrentHp;

        // Ids
        public string UnitId;
        public List<string> TargetIds;

        // Skills
        public List<SkillData> Skills;
        public SkillData SelectedSkill;
        public int SkillCooldown;

        // Stats
        public int Attack;
        public int Defense;
        public int Speed;
        public bool IsAlive;
        public int Fire;
        public int Water;
        public int Earth;
        public int Air;

        // Tick
        public int HealOfTick;
        public int DamageOfTick;
        public List<SkillId> SkillsOfTick;


        public CombatData(string unitId, List<SkillData> skills)
        {
            UnitId = unitId;
            Skills = skills;
            Attack = 1;
            IsAlive = true;
            TargetIds = new List<string>();
        }

        public void AddStatsFromAllEquipments(EquipmentBundle bundle)
        {
            AddWeapon(bundle.Weapon);
            AddEquipmentData(bundle.Head);
            AddEquipmentData(bundle.Feet);
            AddEquipmentData(bundle.OffHand);
            AddEquipmentData(bundle.Pet);
        }

        private void AddWeapon(WeaponEquipmentData weaponData)
        {
            AddEquipmentData(weaponData);
        }

        private void AddEquipmentData(EquipmentData equipmentData)
        {
            if (equipmentData == null) return;
            MaxHp += equipmentData.MaxHp;
            Attack += equipmentData.Attack;
            Defense += equipmentData.Defense;
            Speed += equipmentData.Speed;
            Fire += equipmentData.Fire;
            Water += equipmentData.Water;
            Earth += equipmentData.Earth;
            Air += equipmentData.Air;
        }
    }
}
