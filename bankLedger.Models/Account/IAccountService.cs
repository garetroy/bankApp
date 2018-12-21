using System;
namespace bankLedger.Models
{
    public interface IAccountService
    {
        bool CreateAccount(Account account);
        Account SignIn(string username, string encryptedPassword);
        bool SignOut(Account account);
    }
}
