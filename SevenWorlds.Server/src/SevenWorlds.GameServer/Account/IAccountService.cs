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
        Task<bool> CheckLoginCredentials(string username, string password);
        Task<string> GetPlayerName(string username);
        Task RegisterAccount(string username, string password, string playerName);
    }
}
