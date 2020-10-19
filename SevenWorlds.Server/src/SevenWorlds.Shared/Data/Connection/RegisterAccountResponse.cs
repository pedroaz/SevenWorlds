namespace SevenWorlds.Shared.Data.Connection
{
    public enum RegisterAccountResponseType
    {
        Success,
        UserNameAlreadyExists,
        PlayerNameAlreadyExists
    }

    public class RegisterAccountResponse
    {
        public RegisterAccountResponseType response { get; set; }
    }
}
