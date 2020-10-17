using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum PlayerActionType
    {
        Movement = 0,
        CreateBattleEncounter = 1
    }

    public enum PlayerActionScale
    {
        Universe = 0,
        World = 1,
        Area = 2
    }

    public class PlayerActionData : NetworkData
    {
        public PlayerActionType ActionType { get; set; }
        public PlayerActionScale ActionScale { get; set; }
        public string CharacterId { get; set; }
        public string AreaId { get; set; }
        public WorldPosition Position { get; set; }

        public PlayerActionData(PlayerActionType type, PlayerActionScale scale)
        {
            ActionType = type;
            ActionScale = scale;
        }
    }
}
