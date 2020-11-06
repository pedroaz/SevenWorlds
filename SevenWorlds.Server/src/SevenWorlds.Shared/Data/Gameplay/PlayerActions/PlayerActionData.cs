namespace SevenWorlds.Shared.Data.Gameplay
{

    public enum PlayerActionType
    {
        Attack,
        Harvest,
        Buy,
        Movement
    }

    public class PlayerActionData
    {
        public virtual PlayerActionType GetActionType { get; }
    }
}
