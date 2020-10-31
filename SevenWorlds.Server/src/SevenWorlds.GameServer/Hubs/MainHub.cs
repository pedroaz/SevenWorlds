using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.GameState;
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
        private readonly ICharacterFactory characterFactory;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService,
            IPlayerActionCollection playerActionQueue,
            IServerManager serverManager,
            ILoginService loginService,
            IAccountService accountService,
            ICharacterFactory characterFactory)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
            this.serverManager = serverManager;
            this.loginService = loginService;
            this.accountService = accountService;
            this.characterFactory = characterFactory;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try {
                logService.Log($"Someone disconnected! {Context.ConnectionId}");
                gameStateService.RemovePlayerFromTheGame(Context.ConnectionId);
                return base.OnDisconnected(stopCalled);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }

        }

        #region Admin

        public void AdminStartGameServer(string serverId)
        {
            try {
                logService.Log("Recieved Admin Start Request");
                serverManager.StartServerRequest(serverId);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }

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
            try {
                return await loginService.Login(data, Context.ConnectionId);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public async Task<RegisterAccountResponse> RequestRegisterAccount(RegisterAccountData data)
        {
            try {
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
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }


        public async Task<bool> RequestCreateCharacter(string playerName, string worldId, CharacterType characterType)
        {
            return await characterFactory.NewCharacter(playerName, worldId, characterType);
        }


        #endregion

        #region Syncs

        public UniverseSyncData RequestUniverseSync()
        {
            try {
                return gameStateService.GetUniverseSyncData();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public PlayerData RequestPlayerData(string playerName)
        {
            try {
                return gameStateService.PlayerCollection.FindByName(playerName);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public List<WorldData> RequestAllWorlds()
        {
            try {
                return gameStateService.WorldCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public WorldSyncData RequestWorldSync(string worldId)
        {
            try {
                return gameStateService.GetWorldSyncData(worldId);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public AreaSyncData RequestAreaSync(string areaId, string playerId)
        {
            try {
                return gameStateService.GetAreaSyncData(areaId);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public List<AreaData> RequestAllAreas()
        {
            try {
                return gameStateService.AreaCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public List<PlayerData> RequestAllPlayerDatas()
        {
            try {
                return gameStateService.PlayerCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public SectionBundle RequestSectionBundle()
        {
            try {
                return gameStateService.SectionCollection.Bundle;
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public List<CharacterData> RequestPlayerCharacters(string playerName)
        {
            try {
                return gameStateService.CharacterCollection.FindAllPlayerCharacters(playerName);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        #endregion

        #region General Client Requests

        public bool RequestPing()
        {
            try {
                return true;
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public void RequestSendChatMessage(ChatMessageData data)
        {
            try {
                Clients.All.OnChatMessage(data);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }


        #endregion

        #region Actions
        public void RequestMovementAction(MovementActionData data)
        {
            try {
                logService.Log($"Recieved Movement Request {data.CharacterId}");
                playerActionQueue.AddToBundle(data);
            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }

        public void RequestStartBattleAction(StartBattleActionData data)
        {
            try {
                playerActionQueue.AddToBundle(data);

            }
            catch (Exception e) {
                logService.Log(e);
                throw;
            }
        }


        #endregion
    }
}
