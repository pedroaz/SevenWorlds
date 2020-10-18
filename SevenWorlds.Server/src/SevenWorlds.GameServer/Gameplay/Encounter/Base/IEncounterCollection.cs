using SevenWorlds.GameServer.Gameplay.Encounter.Base;
using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Encounter
{
    public interface IEncounterCollection : IDataCollection<EncounterInstance>
    {
    }
}
