namespace SevenWorlds.Shared.Data.Connection
{
    public class RegisterAccountData
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
