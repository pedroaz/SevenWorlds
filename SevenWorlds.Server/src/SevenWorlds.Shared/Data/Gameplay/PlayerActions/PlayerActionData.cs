using SevenWorlds.Shared.Data.Base;
using System.Collections.Generic;

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
        public List<string> CharactersIds { get; set; }
        public List<string> AreaIds { get; set; }
        public List<WorldPosition> Positions { get; set; }

        public PlayerActionData(PlayerActionType type, PlayerActionScale scale)
        {
            ActionType = type;
            ActionScale = scale;
            CharactersIds = new List<string>();
            AreaIds = new List<string>();
            Positions = new List<WorldPosition>();
        }
    }
}
