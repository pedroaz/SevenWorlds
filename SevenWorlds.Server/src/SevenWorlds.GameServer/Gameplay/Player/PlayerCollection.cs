using SevenWorlds.GameServer.Gameplay.Area;
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

        public IEnumerable<PlayerData> GetAll()
        {
            return players;
        }

        public PlayerData FindByName(string name)
        {
            return players.Find(x => x.Name == name);
        }

        public void Remove(string id)
        {
            players.RemoveAll(x => x.Id == id);
        }
    }
}
