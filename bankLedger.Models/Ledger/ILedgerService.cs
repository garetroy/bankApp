using System.Collections.Generic;

namespace bankLedger.Models
{
    public interface ILedgerService
    {
        ICollection<Ledger> GetAllLedgers(Account account);
        Ledger CreateLedger(Account account, Ledger ledger);
        decimal GetTotalBalance(Account account);
    }
}
