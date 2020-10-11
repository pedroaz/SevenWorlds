using SevenWorlds.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public class CharacterCollection : ICharacterCollection
    {
        public void Add(CharacterData data)
        {
            throw new NotImplementedException();
        }

        public CharacterData FindById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CharacterData> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
