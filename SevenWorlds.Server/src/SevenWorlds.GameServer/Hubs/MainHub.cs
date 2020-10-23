using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Player;
using SevenWorlds.GameServer.Server;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Section;
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IPlayerActionCollection playerActionQueue;
        private readonly IServerManager serverManager;
        private readonly ILoginService loginService;
        private readonly IAccountService accountService;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService,
            IPlayerActionCollection playerActionQueue,
            IServerManager serverManager,
            ILoginService loginService,
            IAccountService accountService)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
            this.serverManager = serverManager;
            this.loginService = loginService;
            this.accountService = accountService;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            logService.Log($"Someone disconnected! {Context.ConnectionId}");
            return base.OnDisconnected(stopCalled);
        }

        #region Admin

        public void AdminStartGameServer(string serverId)
        {
            logService.Log("Recieved Admin Start Request");
            serverManager.StartServerRequest(serverId);
        }

        public void AdminStopGameServer()
        {

        }

        public async Task ResetUniverseFakeData()
        {
            logService.Log("Recieved Admin Reset to Fake Data");
            try {
                await serverManager.ResetFakeData();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
            logService.Log("Finished setting fake data");
        }

        #endregion

        #region Account

        public async Task<LoginResponseData> RequestLogin(LoginData data)
        {
            return await loginService.Login(data, Context.ConnectionId);
        }

        public async Task<RegisterAccountResponse> RequestRegisterAccount(RegisterAccountData data)
        {
            if (await accountService.UsernameExists(data.Username)) {
                return new RegisterAccountResponse() {
                    response = RegisterAccountResponseType.UserNameAlreadyExists
                };
            }

            if (await accountService.PlayerNameExists(data.Username)) {
                return new RegisterAccountResponse() {
                    response = RegisterAccountResponseType.PlayerNameAlreadyExists
                };
            }

            await accountService.RegisterAccount(data.Username, data.Password, data.PlayerName);

            return new RegisterAccountResponse() {
                response = RegisterAccountResponseType.Success
            };
        }


        #endregion

        #region Syncs

        public UniverseSyncData RequestUniverseSync()
        {
            return gameStateService.GetUniverseSyncData();
        }

        public List<WorldData> RequestAllWorlds()
        {
            return gameStateService.WorldCollection.GetAll();
        }

        public WorldSyncData RequestWorldSync(string worldId)
        {
            return gameStateService.GetWorldSyncData(worldId);
        }

        public AreaSyncData RequestAreaSync(string areaId, string playerId)
        {
            return gameStateService.GetAreaSyncData(areaId);
        }

        public List<AreaData> RequestAllAreas()
        {
            return gameStateService.AreaCollection.GetAll();
        }

        public List<PlayerData> RequestAllPlayerDatas()
        {
            return gameStateService.PlayerCollection.GetAll();
        }

        public SectionBundle RequestSectionBundle()
        {
            return gameStateService.SectionCollection.Bundle;
        }

        public List<CharacterData> RequestPlayerCharacters(string playerName)
        {
            return gameStateService.CharacterCollection.FindAllPlayerCharacters(playerName);
        }

        #endregion

        #region General Client Requests

        public bool RequestPing()
        {
            return true;
        }

        public void RequestSendChatMessage(ChatMessageData data)
        {
            Clients.All.OnChatMessage(data);
        }
        #endregion

        #region Actions
        public void RequestMovementAction(MovementActionData data)
        {
            logService.Log($"Recieved Movement Request {data.CharacterId}");
            playerActionQueue.AddToBundle(data);
        }

        public void RequestStartBattleAction(StartBattleActionData data)
        {
            playerActionQueue.AddToBundle(data);
        }
        #endregion
    }
}
