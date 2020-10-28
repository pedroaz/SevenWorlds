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
    }

    public async Task TryToRegister()
    {
        var username = usernameInputField.GetValue();
        var password = passwordInputField.GetValue();
        var playerName = playerNameInputField.GetValue();

        LOG.Log($"Trying to register with: {username} | {password} | {playerName}");

        var res = await NetworkService.Object.Register(username, password, playerName);

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
