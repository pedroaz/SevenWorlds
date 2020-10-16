using SevenWorlds.Shared.Data.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public interface ILoginService
    {
        Task<LoginResponseData> Login(LoginData data, string connectionId);
    }
}
