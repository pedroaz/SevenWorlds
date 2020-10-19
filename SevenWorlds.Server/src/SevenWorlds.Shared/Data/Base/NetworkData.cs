using System;

namespace SevenWorlds.Shared.Data.Base
{
    [Serializable]
    public class NetworkData
    {
        public string Id { get; set; }
        public DateTime TimeOfCreation { get; set; }
    }
}
