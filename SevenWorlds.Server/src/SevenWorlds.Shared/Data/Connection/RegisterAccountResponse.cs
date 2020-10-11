using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
