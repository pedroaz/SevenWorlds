using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Account
{
    public interface IAccountService
    {
        bool UsernameExists(string username);
        bool CheckLogin(string username, string password);
        PlayerData Login(string username);
    }
}
