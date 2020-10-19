using SevenWorlds.Shared.Data.Base;

namespace SevenWorlds.Shared.Data.Connection
{
    public class LoginData : NetworkData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
