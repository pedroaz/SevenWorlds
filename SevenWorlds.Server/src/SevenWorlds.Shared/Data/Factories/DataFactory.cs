using SevenWorlds.Shared.Data.Base;
using System;

namespace SevenWorlds.Shared.Data.Factory
{
    public class DataFactory
    {
        public string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public void SetDefaultValues(NetworkData data)
        {
            data.TimeOfCreation = DateTime.Now;
            data.Id = GetGUID();
        }
    }
}
