using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public interface IPlayerCollection : IDataCollection<PlayerData>
    {
        PlayerData FindByName(string name);
        List<PlayerData> FindAllPlayersByArea(string areaId);
    }
}
