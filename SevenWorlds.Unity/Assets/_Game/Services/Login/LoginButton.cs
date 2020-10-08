﻿using System.Threading.Tasks;

public class LoginButton : GameButton
{
    public override async Task OnClick()
    {
        print("Trying to log in!");

        var response = await LoginService.Object.Login();
        await LoginService.Object.ProcessLoginResponse(response);
    }

   
}