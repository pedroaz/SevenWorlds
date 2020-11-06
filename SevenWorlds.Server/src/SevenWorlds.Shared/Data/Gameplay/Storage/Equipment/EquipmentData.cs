using SevenWorlds.Shared.Data.Gameplay.Storage.Equipment;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum EquipmentId
    {
        Sword
    }

    public enum EquipmentType
    {
        MainHand,
        OffHand,
        Head,
        Feet,
        Aura,
        Pet
    }

    public class EquipmentData
    {
        public string EquipmentName;
        public EquipmentId EquipmentId;
        public EquipmentType Type;
        public int MaxHp;

        public int Attack;
        public int Defense;
        public int Speed;

        public int Fire;
        public int Water;
        public int Earth;
        public int Air;


        public EquipmentData(EquipmentDescription description)
        {
            EquipmentName = description.EquipmentName;
            EquipmentId = description.EquipmentId;
            Type = description.Type;
            MaxHp = description.MaxHp;

            Attack = description.Attack;
            Defense = description.Defense;
            Speed = description.Speed;

            Fire = description.Fire;
            Water = description.Water;
            Earth = description.Earth;
            Air = description.Air;
        }

    }
}
