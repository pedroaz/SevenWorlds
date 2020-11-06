using SevenWorlds.Shared.Data.Gameplay;
using System;

namespace SevenWorlds.GameServer.Gameplay.Construction.Universe
{
    public class UniverseFactory : IUniverseFactory
    {
        public UniverseData CreateNewUniverse(string name)
        {
            var data = new UniverseData() {
                Name = name
            };
            data.SetupDefaultValues();
            return data;
        }
    }
}
