namespace SevenWorlds.GameServer.Database.CollectionsSchemas
{
    public class AccountModel : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }
    }
}
