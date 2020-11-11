namespace SevenWorlds.Shared.Data.Gameplay
{

    public enum PlayerActionType
    {
        Attack,
        Harvest,
        Buy,
        Movement
    }
    
    [System.Serializable]
    public class PlayerActionData
    {
        public string Id;
        public virtual PlayerActionType GetActionType { get; }
    }
}
