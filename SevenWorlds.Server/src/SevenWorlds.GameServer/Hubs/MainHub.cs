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
using SevenWorlds.Shared.Data.Sync;
using SevenWorlds.Shared.Network;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Hubs
{
    [HubName(NetworkConstants.MainHubName)]
    public class MainHub : Hub
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IPlayerActionQueue playerActionQueue;
        private readonly IAccountService accountService;
        private readonly IServerManager serverManager;

        public MainHub(
            ILogService logService,
            IGameStateService gameStateService,
            IPlayerActionQueue playerActionQueue,
            IAccountService accountService,
            IServerManager serverManager)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.playerActionQueue = playerActionQueue;
            this.accountService = accountService;
            this.serverManager = serverManager;
        }

        #region Admin

        public void AdminStartGameServer(string serverId)
        {
            serverManager.StartServerRequest(serverId);
        }

        public void AdminStopGameServer()
        {

        }



        #endregion



        #region Client Requests

        public bool RequestPing()
        {
            return true;
        }

        public async Task<LoginResponseData> RequestLogin(LoginData data)
        {
            if (!await accountService.UsernameExists(data.Username)) {

                return new LoginResponseData() {
                    ResponseType = LoginResponseType.UsernameNotFound
                };
            }

            if (!await accountService.CheckLogin(data.Username, data.Password)) {

                return new LoginResponseData() {
                    ResponseType = LoginResponseType.PasswordIncorrect
                };
            }

            return new LoginResponseData() {
                UniverseSyncData = gameStateService.GetUniverseSyncData(),
                PlayerData = await accountService.Login(data.Username, Context.ConnectionId),
                ResponseType = LoginResponseType.Success
            };
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

        public void RequestAddCharacterToWorld()
        {

        }

        public void RequestSendChatMessage(ChatMessageData data)
        {
            logService.Log("Recieved Chat Message Command");
            Clients.All.OnChatMessage(data);
        }

        public UniverseSyncData RequestUniverseSync()
        {
            return gameStateService.GetUniverseSyncData();
        }

        public WorldSyncData RequestWorldSync(string worldId)
        {
            return gameStateService.GetWorldSyncData(worldId);
        }

        public AreaSyncData RequestAreaSync(string areaId, string playerId)
        {
            return gameStateService.GetAreaSyncData(areaId);
        }

        public PlayerActionStatusData RequestStartPlayerAction(PlayerActionData playerActionData)
        {
            return playerActionQueue.AddToQueue(playerActionData);
        }

        #endregion


    }
}
