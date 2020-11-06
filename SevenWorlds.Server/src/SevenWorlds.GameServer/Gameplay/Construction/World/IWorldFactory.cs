using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Construction.World
{
    public interface IWorldFactory
    {
        WorldData CreateNewWorld(string name, string universeId, int index);
    }
}
