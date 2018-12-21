using System;
namespace bankLedger.Models
{
    public class Account
    {
        public Account(string userName, string password, string salt,
                        DateTime? lastLogin)
        {
            UserName = userName;
            Password = password;
            Salt = salt;
            LastLogin = lastLogin;
        }

        public string UserName { get; }
        public string Password { get; }
        public string Salt { get; }
        public DateTime? LastLogin { get; }
    }
}
