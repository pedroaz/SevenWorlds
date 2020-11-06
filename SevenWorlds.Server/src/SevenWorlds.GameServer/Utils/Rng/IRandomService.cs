namespace SevenWorlds.GameServer.Utils.Rng
{
    public interface IRandomService
    {
        int GetRandomInt(int min, int max);
        bool FlipCoin();
    }
}
