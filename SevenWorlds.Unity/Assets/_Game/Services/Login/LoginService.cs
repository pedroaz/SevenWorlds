﻿using SevenWorlds.Shared.Data.Connection;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoginService : GameService<LoginService>
{
    public TMP_InputField InputField;

    private void Awake()
    {
        Object = this;
    }

    public async Task<LoginResponseData> Login()
    {
        return await NetworkService.Object.Login(new LoginData(InputField.text, "123") {
        });
    }

    public async Task ProcessLoginResponse(LoginResponseData response)
    {
        if (response.ResponseType == LoginResponseType.Success) {
            print("Log in was success!");
            GameState.Object.PlayerData = response.PlayerData;
            GameState.Object.Universe = response.UniverseSyncData.Universe;
            GameState.Object.Worlds = response.UniverseSyncData.Worlds;
            GeneralRefresherService.Object.Refresh();
            await ScreenChangerService.Object.ChangeScreen(ScreenId.Universe);
        }
        else {
            print("Log failed");
        }
    }
}
