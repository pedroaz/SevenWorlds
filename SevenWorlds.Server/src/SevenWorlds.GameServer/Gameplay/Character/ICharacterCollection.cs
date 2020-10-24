using SevenWorlds.GameServer.Utils.DataCollections;
using SevenWorlds.Shared.Data.Gameplay;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public interface ICharacterCollection : IDataCollection<CharacterData>
    {
        List<CharacterData> FindAllPlayerCharacters(string playerName);
        void RemovePlayerCharacters(string playerId);
    }
}
