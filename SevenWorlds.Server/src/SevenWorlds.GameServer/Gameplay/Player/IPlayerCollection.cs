﻿using SevenWorlds.GameServer.Utils.DataCollections;
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
        PlayerData FindByPlayerName(string playerName);
        PlayerData FindByConnectionId(string connectionId);
        void RemovePlayer(string playerName);
    }
}
