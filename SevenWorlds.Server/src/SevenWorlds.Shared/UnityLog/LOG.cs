using UnityEngine;

namespace SevenWorlds.Shared.UnityLog
{
    public static class LOG
    {
        public static void Log(object obj)
        {
            Debug.Log(obj.ToString());
        }
    }
}
