using SevenWorlds.Shared.Data.Connection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginService : GameService<LoginService>
{
    public TMP_InputField InputField;

    private void Awake()
    {
        Object = this;
    }

    public async void Login()
    {
        await NetworkService.Object.Login(new LoginData() {
            PlayerName = InputField.text
        });
    }
}
