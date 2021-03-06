﻿using SevenWorlds.GameServer.Gameplay.Area;
using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerCollection : IPlayerCollection
    {
        private List<PlayerData> players;

        public PlayerCollection()
        {
            players = new List<PlayerData>();
        }

        /// <summary>
        /// Insert player into the world
        /// </summary>
        /// <param name="data"></param>
        public void Add(PlayerData data)
        {
            players.Add(data);
        }

        public PlayerData FindById(string id)
        {
            return players.Find(x => x.Id == id);
        }

        public List<PlayerData> GetAll()
        {
            return players;
        }

        public PlayerData FindByPlayerName(string name)
        {
            return players.Find(x => x.PlayerName == name);
        }

        public void Remove(string id)
        {
            players.RemoveAll(x => x.Id == id);
        }

        public PlayerData FindByConnectionId(string connectionId)
        {
            return players?.Find(x => x.ConnectionId == connectionId);
        }

        public void RemovePlayer(string playerName)
        {
            players.RemoveAll(x => x.PlayerName == playerName);
        }
    }
}
