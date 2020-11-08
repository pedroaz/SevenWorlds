namespace SevenWorlds.GameServer.Utils.Rng
{
    public interface IRandomService
    {
        /// <summary>
        /// Get random number between min and max (max inclusive)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        int GetRandomInt(int min, int max);
        bool FlipCoin();
        bool OneInX(int x);
    }
}
