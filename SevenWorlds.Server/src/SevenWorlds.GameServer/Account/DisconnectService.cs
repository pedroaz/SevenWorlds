using SevenWorlds.GameServer.Database;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public class DisconnectService : IDisconnectService
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IDatabaseService databaseService;

        public DisconnectService(ILogService logService, IGameStateService gameStateService, IDatabaseService databaseService)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.databaseService = databaseService;
        }

        public async Task DisconnectPlayer(string connectionId)
        {
            logService.Log($"Disconnecting connection id: {connectionId}", type: LogType.Disconnect);

            var playerData = gameStateService.PlayerCollection.FindByConnectionId(connectionId);

            if (playerData != null) {
                logService.Log($"Disconnecting player: {playerData.PlayerName}", type: LogType.Disconnect);
                await databaseService.UpdatePlayer(playerData);
                gameStateService.RemovePlayerFromTheGame(connectionId);
            }
            else {
                logService.Log($"No player data found for connection id: {connectionId}", type: LogType.Disconnect);
            }
        }
    }
}
