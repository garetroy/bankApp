using System;
using System.Web;
using System.Web.SessionState;

namespace bankLedger.Models
{
    public interface IAccountService
    {
        Account CreateAccount(string userName, string decryptedPassowrd);
        Account SignIn(string userName, string decryptedPassowrd, HttpSessionStateBase session);
        Account IsSignedIn(HttpSessionStateBase session);
        void SignOut(HttpSessionStateBase session);
    }
}
