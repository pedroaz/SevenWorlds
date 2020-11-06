using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SevenWorlds.GameServer.Account;
using SevenWorlds.GameServer.Gameplay.Actions.Base;
using SevenWorlds.GameServer.Gameplay.Character;
using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Gameplay.Quests;
using SevenWorlds.GameServer.Server;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Chat;
using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.ActionDatas;
using SevenWorlds.Shared.Data.Gameplay.Quests;
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
        private readonly IQuestGiver questGiver;
        private readonly IDisconnectService disconnectService;
        private readonly IHubService hubService;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService,
            IPlayerActionCollection playerActionQueue,
            IServerManager serverManager,
            ILoginService loginService,
            IAccountService accountService,
            ICharacterFactory characterFactory,
            IQuestGiver questGiver,
            IDisconnectService disconnectService,
            IHubService hubService)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
            this.serverManager = serverManager;
            this.loginService = loginService;
            this.accountService = accountService;
            this.characterFactory = characterFactory;
            this.questGiver = questGiver;
            this.disconnectService = disconnectService;
            this.hubService = hubService;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try {
                logService.Log($"Someone disconnected! {Context.ConnectionId}", type: LogType.Disconnect);
                disconnectService.DisconnectPlayer(Context.ConnectionId);
                return base.OnDisconnected(stopCalled);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
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
            }

        }

        public void AdminStopGameServer()
        {
            try {

            }
            catch (Exception e) {
                logService.Log(e);
            }
        }

        public async Task ResetUniverseFakeData()
        {
            logService.Log("Recieved Admin Reset Master Fake Data");
            try {
                await serverManager.ResetMasterData();
                logService.Log("Finished setting Master Data");
            }
            catch (Exception e) {
                logService.Log(e);
            }

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
                return null;
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
                return null;
            }
        }


        public async Task<CharacterData> RequestCreateCharacter(string playerName, string worldId, CharacterType characterType)
        {
            try {
                return await characterFactory.NewCharacter(playerName, worldId, characterType);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
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
                return null;
            }
        }

        public PlayerData RequestPlayerData(string playerName)
        {
            try {
                return gameStateService.PlayerCollection.FindByPlayerName(playerName);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public List<WorldData> RequestAllWorlds()
        {
            try {
                return gameStateService.WorldCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public WorldSyncData RequestWorldSync(string worldId)
        {
            try {
                return gameStateService.GetWorldSyncData(worldId);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public AreaSyncData RequestAreaSync(string areaId, string playerName)
        {
            try {
                return gameStateService.GetAreaSyncData(areaId);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public List<AreaData> RequestAllAreas()
        {
            try {
                return gameStateService.AreaCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public List<PlayerData> RequestAllPlayerDatas()
        {
            try {
                return gameStateService.PlayerCollection.GetAll();
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public SectionBundle RequestSectionBundle()
        {
            try {
                return gameStateService.SectionCollection.Bundle;
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public List<CharacterData> RequestPlayerCharacters(string playerName)
        {
            try {
                List<CharacterData> characters = gameStateService.CharacterCollection.FindAllPlayerCharacters(playerName);
                return characters;
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
            }
        }

        public List<QuestData> RequestPlayerQuests(string playerName)
        {
            try {
                return questGiver.GetQuests(playerName);
            }
            catch (Exception e) {
                logService.Log(e);
                return null;
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
                return false;
            }
        }

        public void RequestSendChatMessage(ChatMessageData data)
        {
            try {
                Clients.All.OnChatMessage(data);
            }
            catch (Exception e) {
                logService.Log(e);
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
            }
        }

        public void RequestStartBattleAction(StartBattleActionData data)
        {
            try {
                playerActionQueue.AddToBundle(data);

            }
            catch (Exception e) {
                logService.Log(e);
            }
        }

        public void RequestStartQuest(string playerName, QuestId questId)
        {
            try {
                var player = gameStateService.PlayerCollection.FindByPlayerName(playerName);
                player.StartQuest(questId);
            }
            catch (Exception e) {
                logService.Log(e);
            }

        }

        public bool RequestCollectQuest(string playerName, QuestId questId)
        {
            try {
                var player = gameStateService.PlayerCollection.FindByPlayerName(playerName);
                var result = player.CollectQuest(questId);
                hubService.PlayerDataSync(player);
                return result;
            }
            catch (Exception e) {
                logService.Log(e);
                return false;
            }

        }
        #endregion
    }
}
