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
        Task<bool> UsernameExists(string username);
        Task<bool> PlayerNameExists(string playerName);
        Task<bool> CheckLogin(string username, string password);
        Task<PlayerData> Login(string username, string connectionId);
        Task RegisterAccount(string username, string password, string playerName);
    }
}
