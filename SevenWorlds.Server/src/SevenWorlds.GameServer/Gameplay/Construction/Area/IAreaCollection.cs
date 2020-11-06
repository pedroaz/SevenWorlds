using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Area
{
    public interface IAreaCollection : IDataCollection<AreaData>
    {
        List<AreaData> GetAllAreasFromWorld(string worldId);
        AreaData FindByPosition(Position position);
        AreaData FindCityOfWorld(string worldId);
    }
}
