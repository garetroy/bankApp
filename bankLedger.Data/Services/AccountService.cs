using System;
using System.Web.SessionState;
using bankLedger.Data.DbObject;
using bankLedger.Data.Mappers;
using bankLedger.Models;
namespace bankLedger.Data.Services
{
    public sealed class AccountService : IAccountService
    {
        public AccountService(IBankLedgerService service)
        {
            BankLedgerService = service;
        }

        public Account CreateAccount(string userName, string decryptedPassword)
        {
            var dbKey = $"USER_{userName.ToLower()}";

            //The account already exists
            if (BankLedgerService.DataBase.ContainsKey(dbKey))
                return null;

            var encryptedPasswordAndSalt = HashService.
                ComputeHash(decryptedPassword);

            var dbAccount = new DbAccount
            {
                User_Name = userName,
                Encrypted_Password = encryptedPasswordAndSalt.Item1,
                Salt = encryptedPasswordAndSalt.Item2,
                Last_Login = DateTime.Now
            };

            var account = new Account(userName, encryptedPasswordAndSalt.Item1,
                          encryptedPasswordAndSalt.Item2, dbAccount.Last_Login);

            BankLedgerService.DataBase[dbKey] = dbAccount;

            return account;
        }

        public Account SignIn(string userName, string decryptedPassword,
                HttpSessionState session)
        {
            var dbKey = $"USER_{userName.ToLower()}";

            //Account dosen't exist
            if (!BankLedgerService.DataBase.ContainsKey(dbKey))
                return null;

            var account = AccountMapper.Map((DbAccount)BankLedgerService.DataBase[dbKey]);

            if(HashService.VerifyHash(decryptedPassword, 
                                                account.EncryptedPassword,
                                                account.Salt))
            {
                session["CURRENTUSER"] = account;
                return account;
            }

            return null;
        }

        public bool SignOut(Account account, HttpSessionState session)
        {
            if (((DbAccount)session["CURRENTUSER"]).User_Name != account.UserName)
                return false;

            session["CURRENTUSER"] = null;
            return true;
        }

        public IBankLedgerService BankLedgerService { get; }
    }
}
