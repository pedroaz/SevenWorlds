﻿using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Character
{
    public interface ICharacterFactory
    {
        Task<CharacterData> NewCharacter(string playerName, string worldId, CharacterType characterType);
        void RefreshCharacter(CharacterData data);
        void SetupStorage();
    }
}
