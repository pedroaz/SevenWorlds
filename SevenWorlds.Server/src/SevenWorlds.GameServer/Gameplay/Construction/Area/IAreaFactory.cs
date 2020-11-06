using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Construction.Area
{
    public interface IAreaFactory
    {
        AreaData CreateNewArea(string name, Position position, string worldId, AreaType type);
    }
}
