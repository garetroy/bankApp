using System;
using System.Collections.Generic;
using bankLedger.Models;

namespace bankLedger.Data.Services
{
    public class BankLedgerService : IBankLedgerService
    {
        public BankLedgerService(Dictionary<string,object> db)
        {
            AccountService = new AccountService(this);
            LedgerService = new LedgerService(this);
            HashService = new HashService();
            DataBase = db;
        }

        public IAccountService AccountService { get; }
        public ILedgerService LedgerService { get; }
        public IHashService HashService { get; }

        //This is because persistant data is not implemented
        public Dictionary<string, object> DataBase { get; set; }
    }
}
