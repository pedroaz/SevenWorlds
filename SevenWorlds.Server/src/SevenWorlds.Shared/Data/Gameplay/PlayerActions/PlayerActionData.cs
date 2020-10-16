using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum PlayerActionType
    {
        Movement = 0,
        CreateBattleEncounter = 1
    }

    public class PlayerActionData : NetworkData
    {
        public PlayerActionType ActionType { get; set; }
    }
}
