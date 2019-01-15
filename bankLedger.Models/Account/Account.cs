using System;

namespace bankLedger.Models
{
    public class Account
    {
        public Account(string userName, string encryptedPassword, string salt,
                        DateTime? lastLogin)
        {
            UserName = userName;
            EncryptedPassword = encryptedPassword;
            Salt = salt;
            LastLogin = lastLogin;
        }

        public string UserName { get; }
        public string EncryptedPassword { get; }
        public string Salt { get; }
        public DateTime? LastLogin { get; }
    }
}