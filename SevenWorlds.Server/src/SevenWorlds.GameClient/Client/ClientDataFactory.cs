using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;

namespace SevenWorlds.Shared.Data.Factory
{
    public class ClientDataFactory
    {
        public ChatMessageData CreateChatMessageData(string playerName, string message)
        {
            var data = new ChatMessageData(playerName, message);
            return data;
        }

        public MovementActionData CreateMovementActionData(string characterId, string fromAreaId, string toAreaId)
        {
            var data = new MovementActionData(characterId, fromAreaId, toAreaId);
            return data;
        }

        public SelectSkillActionData CreateSelectSkillActionData()
        {
            return new SelectSkillActionData();
        }

        public RegisterAccountData CreateRegisterAccountData(string username, string password, string playerName)
        {
            RegisterAccountData data = new RegisterAccountData(username, password, playerName);
            return data;
        }

        public CharacterData CreateNewCharacter(string playerName, CharacterDescription characterDescription)
        {
            CharacterData data = new CharacterData(playerName, characterDescription);
            return data;
        }
    }
}
