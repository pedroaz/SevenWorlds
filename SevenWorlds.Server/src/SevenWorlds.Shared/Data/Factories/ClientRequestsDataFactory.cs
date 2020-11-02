using SevenWorlds.Shared.Data.Base;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Character;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SevenWorlds.Shared.Data.Factory
{
    public class ClientRequestsDataFactory : DataFactory
    {
        public ChatMessageData CreateChatMessageData(string playerName, string message)
        {
            var data = new ChatMessageData(playerName, message);
            SetDefaultValues(data);
            return data;
        }

        public MovementActionData CreateMovementActionData(string characterId, string fromAreaId, string toAreaId)
        {
            var data = new MovementActionData(characterId, fromAreaId, toAreaId);
            SetDefaultValues(data);
            return data;
        }

        public SelectSkillActionData CreateSelectSkillActionData()
        {
            return new SelectSkillActionData();
        }

        public RegisterAccountData CreateRegisterAccountData(string username, string password, string playerName)
        {
            RegisterAccountData data = new RegisterAccountData(username, password, playerName);
            SetDefaultValues(data);
            return data;
        }

        public CharacterData CreateNewCharacter(string playerName, CharacterDescription characterDescription)
        {
            CharacterData data = new CharacterData(playerName, characterDescription);
            return data;
        }
    }
}
