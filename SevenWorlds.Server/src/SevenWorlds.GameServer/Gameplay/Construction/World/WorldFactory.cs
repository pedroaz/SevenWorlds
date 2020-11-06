using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Construction.World
{
    public class WorldFactory : IWorldFactory
    {
        public WorldData CreateNewWorld(string name, string universeId, int index)
        {
            var data = new WorldData() {
                Name = name,
                UniverseId = universeId,
                WorldIndex = index
            };
            data.SetupDefaultValues();
            return data;
        }
    }
}
