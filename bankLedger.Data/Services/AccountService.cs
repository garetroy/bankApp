﻿using System;
using System.Collections.Generic;
using System.Web;
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
            BankLedgerService = service ?? throw new ArgumentException("Cannot instantiate AccountService.");
        }

        public Account CreateAccount(string userName, string decryptedPassword)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username was null or whitespace");

            if (string.IsNullOrWhiteSpace(decryptedPassword))
                throw new ArgumentException("Password was null or whitespace");

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
                HttpSessionStateBase session)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username was null or whitespace");

            if (string.IsNullOrWhiteSpace(decryptedPassword))
                throw new ArgumentException("Password was null or whitespace");

            if(session == null)
                throw new ArgumentException("Session was null");


            var dbKey = $"USER_{userName.ToLower()}";

            //Account dosen't exist
            if (!BankLedgerService.DataBase.ContainsKey(dbKey))
                return null;

            //Account already loggedin
            if (((Account)session["CURRENTUSER"])?.UserName == userName)
                return null;

            var account = AccountMapper.Map((DbAccount)BankLedgerService.DataBase[dbKey]);

            if(HashService.VerifyHash(decryptedPassword, 
                                                account.EncryptedPassword,
                                                account.Salt))
            {
                ((DbAccount)BankLedgerService.DataBase[dbKey]).Last_Login = DateTime.Now;
                session["CURRENTUSER"] = account;
                return account;
            }

            return null;
        }

        public bool SignOut(Account account, HttpSessionStateBase session)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.UserName))
                throw new ArgumentException("Account or Username was invalid/null");

            if (session == null)
                throw new ArgumentException("Session was null");

            //Cannot logout an account that wasn't already logged in.
            if (((Account)session["CURRENTUSER"])?.UserName != account.UserName)
                return false;

            session["CURRENTUSER"] = null;
            return true;
        }

        public IBankLedgerService BankLedgerService { get; }
    }
}
