using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoginScreenRefresherService : GameService<LoginScreenRefresherService>
{
    private void Awake()
    {
        Object = this;
    }

    public static void Refresh()
    {
        LoginService.HideLoginResponse();
    }
}
