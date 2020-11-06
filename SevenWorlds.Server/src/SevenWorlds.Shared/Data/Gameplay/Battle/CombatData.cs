using SevenWorlds.Shared.Data.Gameplay.Equipment;
using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class CombatData
    {
        public int MaxHp;
        public int CurrentHp;

        public string UnitId;
        public List<string> TargetIds;

        public SkillData SelectedSkill;
        public int Attack;
        public int Defense;
        public int Speed;
        public bool IsAlive;

        public int Fire;
        public int Water;
        public int Earth;
        public int Air;

        // List of skills
        public List<SkillData> Skills;

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
            AddEquipmentData(bundle.Aura);
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
