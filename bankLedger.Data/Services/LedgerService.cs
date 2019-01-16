using bankLedger.Data.DbObject;
using bankLedger.Data.Mappers;
using bankLedger.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bankLedger.Data.Services
{
    public sealed class LedgerService : ILedgerService
    {
        public LedgerService(IBankLedgerService service)
        {
            BankLedgerService = service ?? throw new ArgumentException("Cannot instantiate LedgerService");
        }

        public ICollection<Ledger> GetAllLedgers(Account account)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.UserName))
                throw new ArgumentException("Account Null or Username invalid");

            var ledgerRootName = $"{account.UserName}_Ledger_";

            IEnumerable<DbLedger> userDbLedgers = BankLedgerService.DataBase
                .Where(x => x.Key.Contains(ledgerRootName)).Select(x => (DbLedger)x.Value);

            if (!userDbLedgers.Any())
                return null;

            List<Ledger> ledgers = userDbLedgers.Select(LedgerMapper.Map).ToList();

            return ledgers;
        }

        public Ledger CreateLedger(Account account, Ledger ledger)
        {
            if (ledger == null)
                throw new ArgumentException("Ledger Null");

            if (account == null || string.IsNullOrWhiteSpace(account.UserName))
                throw new ArgumentException("Account Null or Username invalid");

            var ledgerCountKey = $"{account.UserName.ToLower()}_ledgerCount";
            ulong ledgerCount = BankLedgerService.DataBase.ContainsKey(ledgerCountKey)
                                ? (ulong)BankLedgerService.DataBase[ledgerCountKey] : 0;
            ledgerCount += 1;

            var newLedger = new Ledger(ledgerCount, ledger.TransactionType, ledger.Amount);
            var dbLedger = new DbLedger
            {
                Ledger_Id = ledgerCount,
                Transaction_Type = (int)ledger.TransactionType,
                Amount = ledger.Amount
            };

            var ledgerKey = $"{account.UserName.ToLower()}_Ledger_{ledgerCount}";
            if (BankLedgerService.DataBase.ContainsKey(ledgerKey))
                return null;

            BankLedgerService.DataBase[ledgerKey] = dbLedger;
            BankLedgerService.DataBase[ledgerCountKey] = ledgerCount;

            return newLedger;
        }

        public decimal GetTotalBalance(Account account)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.UserName))
                throw new ArgumentException("Account Null or Username invalid");

            var ledgerRootName = $"{account.UserName}_Ledger_";

            IEnumerable<DbLedger> userDbLedgers = BankLedgerService.DataBase.
                Where(x => x.Key.Contains(ledgerRootName)).Select(x => (DbLedger)x.Value);

            decimal sum = 0;
            foreach (DbLedger ledger in userDbLedgers)
                if (ledger.Transaction_Type == (int)TransactionType.Deposit)
                    sum += ledger.Amount;
                else
                    sum -= ledger.Amount;

            return sum;
        }

        public IBankLedgerService BankLedgerService { get; }
    }
}