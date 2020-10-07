﻿using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player.Actions;
using SevenWorlds.GameServer.Hubs;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.PlayerActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public class PlayerActionFactory : IPlayerActionFactory
    {
        private readonly IGameStateService gameStateService;
        private readonly IHubService hubService;

        public PlayerActionFactory(IGameStateService gameStateService, IHubService hubService)
        {
            this.gameStateService = gameStateService;
            this.hubService = hubService;
        }

        public PlayerAction GenerateAction(PlayerActionData data)
        {
            switch (data.ActionType) {
                case PlayerActionType.Movement:
                    return new PlayerMovementAction( (PlayerMovementActionData) data, gameStateService, hubService);
                case PlayerActionType.Attack:
                    return new PlayerAttackAction(data, gameStateService, hubService);
                default:
                    return new PlayerAction(data, gameStateService, hubService);
            }
        }
    }
}
