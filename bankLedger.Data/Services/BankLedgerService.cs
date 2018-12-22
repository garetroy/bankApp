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
            DataBase = db;
        }

        public IAccountService AccountService { get; }
        public ILedgerService LedgerService { get; }

        //This is because persistant data is not implemented
        public IDictionary<string, object> DataBase { get; set; }
    }
}
