using System;
using bankLedger.Models;
namespace bankLedger.Data.Services
{
    public sealed class AccountService : IAccountService
    {
        public bool CreateAccount(Account account)
        {
            return false;
        }

        public Account SignIn(string username, string encryptedPassword)
        {
            return null;
        }

        public bool SignOut(Account account)
        {
            return false;
        }
    }
}
