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
    public GameText loginResponseText;

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
        if (NetworkService.IsConnected) {
            return await NetworkService.Login(new LoginData(Object.usernameInputField.GetValue(), Object.passwordInputField.GetValue()));
        }
        else {
            ShowLoginResponse($"Login failed: Not Connected to server");
            return null;
        }
    }

    public static async Task ProcessLoginResponse(LoginResponseData response)
    {
        LOG.Log("Processing login response");

        if (response.ResponseType == LoginResponseType.Success) {
            LOG.Log("Log in was success!");
            await GameState.RefreshGameStateFromLoginResponse(response);
            GeneralRefresherService.Refresh();
            await ScreenChangerService.ChangeScreen(ScreenId.Universe);
        }
        else {
            LOG.Log($"Log failed {response.ResponseType}");
            ShowLoginResponse($"Login failed: {response.ResponseType}");   
        }
    }

    public static void ShowLoginResponse(string message)
    {
        Object.loginResponseText.Show(true);
        Object.loginResponseText.SetText(message);
    }

    public static void HideLoginResponse()
    {
        Object.loginResponseText.Show(false);
    }
}
