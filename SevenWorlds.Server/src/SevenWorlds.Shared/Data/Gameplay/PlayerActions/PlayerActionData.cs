using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{

    public enum PlayerActionType
    {
        Attack,
        Harvest,
        Buy,
        Movement
    }

    public class PlayerActionData : NetworkData
    {
        public virtual PlayerActionType GetActionType { get; }
    }
}
