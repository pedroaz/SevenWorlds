using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public class CharacterCollection : ICharacterCollection
    {
        private List<CharacterData> characters;

        public CharacterCollection()
        {
            characters = new List<CharacterData>();
        }

        public void Add(CharacterData data)
        {
            characters.Add(data);
        }

        public List<CharacterData> FindAllPlayerCharacters(string playerName)
        {
            return characters.FindAll(x => x.PlayerName == playerName);
        }

        public CharacterData FindById(string id)
        {
            return characters.Find(x => x.Id == id);
        }

        public List<CharacterData> GetAll()
        {
            return characters;
        }

        public void Remove(string id)
        {
            characters.RemoveAll(x => x.Id == id);
        }
    }
}
