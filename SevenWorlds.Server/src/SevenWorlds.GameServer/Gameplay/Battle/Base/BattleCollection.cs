using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Encounters;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Encounter
{
    public class BattleCollection : IBattleCollection
    {
        private List<BattleData> battles { get; set; } = new List<BattleData>();

        public void Add(BattleData data)
        {
            battles.Add(data);
        }

        public BattleData FindById(string id)
        {
            return battles.Find(x => x.Id == id);
        }

        public List<BattleData> GetAll()
        {
            return battles;
        }

        public void Remove(string id)
        {
            battles.RemoveAll(x => x.Id == id);
        }
    }
}
