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

    public static async Task TryToRegister()
    {
        var username = Object.usernameInputField.GetValue();
        var playerName = Object.playerNameInputField.GetValue();
        var password = Object.passwordInputField.GetValue();

        LOG.Log($"Trying to register with: {username} | {password} | {playerName}");

        var res = await NetworkService.Register(username, password, playerName);

        switch (res.response) {
            case RegisterAccountResponseType.Success:
                LOG.Log($"Registration was ok!");
                break;
            case RegisterAccountResponseType.UserNameAlreadyExists:
                LOG.Log($"Username already exists");
                break;
            case RegisterAccountResponseType.PlayerNameAlreadyExists:
                LOG.Log($"Player name already exists");
                break;
        }
    }
}
