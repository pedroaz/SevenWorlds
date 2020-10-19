using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public class EncounterData : NetworkData
    {
        public EncounterType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public enum EncounterType
        {
            Battle
        }

        public virtual List<PlayerActionType> GetPossiblePlayerActions()
        {
            return new List<PlayerActionType>();
        }
       
    }
}
