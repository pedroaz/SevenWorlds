using SevenWorlds.Shared.Data.Base;
using System;

namespace SevenWorlds.Shared.Data.Factory
{
    public class DataFactory
    {
        internal string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        internal void SetDefaultValues(NetworkData data)
        {
            data.TimeOfCreation = DateTime.Now;
            data.Id = GetGUID();
        }
    }
}
