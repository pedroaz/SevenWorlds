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
    }

    public async Task<LoginResponseData> TryToLogin()
    {
        LOG.Log("Trying to log in");
        return await NetworkService.Object.Login(new LoginData(usernameInputField.GetValue(), passwordInputField.GetValue()));
    }

    public async Task ProcessLoginResponse(LoginResponseData response)
    {
        LOG.Log("Processing login response");

        if (response.ResponseType == LoginResponseType.Success) {
            LOG.Log("Log in was success!");
            GameState.Object.PlayerData = response.PlayerData;
            GameState.Object.Universe = response.UniverseSyncData.Universe;
            GameState.Object.Worlds = response.UniverseSyncData.Worlds;
            GameState.Object.Characters = await NetworkService.Object.RequestPlayerCharacters(response.PlayerData.PlayerName);
            GeneralRefresherService.Object.Refresh();
            await ScreenChangerService.Object.ChangeScreen(ScreenId.Universe);
        }
        else {
            LOG.Log($"Log failed {response.ResponseType}");
        }
    }
}
