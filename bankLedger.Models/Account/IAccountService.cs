using System;
using System.Web.SessionState;

namespace bankLedger.Models
{
    public interface IAccountService
    {
        Account CreateAccount(string userName, string decryptedPassowrd);
        Account SignIn(string userName, string decryptedPassowrd, HttpSessionState session);
        bool SignOut(Account account, HttpSessionState session);
    }
}
