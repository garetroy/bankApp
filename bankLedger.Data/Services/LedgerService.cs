using System;
using System.Collections.Generic;
using bankLedger.Models;

namespace bankLedger.Data.Services
{
    public sealed class LedgerService : ILedgerService
    {
        public LedgerService(IBankLedgerService service)
        {
            BankLedgerService = service;
        }

        public ICollection<Ledger> GetAllLedgers(Account account)
        {
            return null;
        }

        public bool CreateLedger(Account account, Ledger ledger)
        {
            return false;
        }

        public decimal GetTotalBalance(Account account)
        {
            return 0;
        }

        public IBankLedgerService BankLedgerService { get; }
    }
}
