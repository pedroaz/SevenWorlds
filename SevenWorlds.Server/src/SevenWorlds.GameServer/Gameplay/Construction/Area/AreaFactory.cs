using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Construction.Area
{
    public class AreaFactory : IAreaFactory
    {
        public AreaData CreateNewArea(string name, Position position, string worldId, AreaType type)
        {
            var data = new AreaData() {
                Name = name,
                Position = position,
                WorldId = worldId,
                Type = type
            };
            data.SetupDefaultValues();
            return data;
        }
    }
}
