using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginService : GameService<LoginService>
{
    public GameInputField usernameInputField;
    public GameInputField passwordInputField;

    private void Awake()
    {
        Object = this;
        if(usernameInputField == null || passwordInputField == null) {
            LOG.Log("Missing fields on LoginService", LogLevel.Error);
        }
    }

    public static async Task<LoginResponseData> TryToLogin()
    {
        LOG.Log("Trying to log in");
        return await NetworkService.Login(new LoginData(Object.usernameInputField.GetValue(), Object.passwordInputField.GetValue()));
    }

    public static async Task ProcessLoginResponse(LoginResponseData response)
    {
        LOG.Log("Processing login response");

        if (response.ResponseType == LoginResponseType.Success) {
            LOG.Log("Log in was success!");
            GameState.PlayerData = response.PlayerData;
            GameState.Universe = response.UniverseSyncData.Universe;
            GameState.Worlds = response.UniverseSyncData.Worlds;
            GameState.Characters = await NetworkService.RequestPlayerCharacters(response.PlayerData.PlayerName);
            GeneralRefresherService.Object.Refresh();
            await ScreenChangerService.ChangeScreen(ScreenId.Universe);
        }
        else {
            LOG.Log($"Log failed {response.ResponseType}");
        }
    }
}
