using System;
using bankLedger.Models;
namespace bankLedger.Data.Services
{
    public sealed class AccountService : IAccountService
    {
        public AccountService(IBankLedgerService service)
        {
            BankLedgerService = service;
        }

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

        public IBankLedgerService BankLedgerService { get; }
    }
}
