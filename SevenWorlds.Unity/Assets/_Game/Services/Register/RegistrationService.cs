using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.UnityLog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RegistrationService : GameService<RegistrationService>
{
    public GameInputField usernameInputField;
    public GameInputField passwordInputField;
    public GameInputField playerNameInputField;

    private void Awake()
    {
        Object = this;
        if(usernameInputField == null || passwordInputField == null || playerNameInputField == null) {
            LOG.Log("Missing fields on Registration Service", LogLevel.Error);
        }
    }

    public static async Task<RegisterAccountResponse> TryToRegister()
    {
        var username = Object.usernameInputField.GetValue();
        var playerName = Object.playerNameInputField.GetValue();
        var password = Object.passwordInputField.GetValue();

        LOG.Log($"Trying to register with: {username} | {password} | {playerName}");

        var res = await NetworkService.Register(username, password, playerName);

        string message = "";

        switch (res.response) {
            case RegisterAccountResponseType.Success:
                message = $"Registration was ok!";
                break;
            case RegisterAccountResponseType.UserNameAlreadyExists:
                message = $"Username already exists";
                break;
            case RegisterAccountResponseType.PlayerNameAlreadyExists:
                message = $"Player name already exists";
                break;
        }

        LOG.Log(message);
        UIEvents.ChangeGameText(GameTextId.RegisterResponse, message);

        return res;
    }
}
