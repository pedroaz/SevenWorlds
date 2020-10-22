using SevenWorlds.Shared.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.Shared.Data.Connection
{
    public class RegisterAccountData : NetworkData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }

        public RegisterAccountData(string username, string password, string playerName)
        {
            Username = username;
            Password = password;
            PlayerName = playerName;
        }
    }
}
