using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Construction.Universe
{
    public interface IUniverseFactory
    {
        UniverseData CreateNewUniverse(string name);
    }
}
