using System;
using bankLedger.Data.DbObject;
using bankLedger.Models;

namespace bankLedger.Data.Mappers
{
    internal sealed class LedgerMapper
    {
        public static Ledger Map(DbLedger ledger)
        {
            return new Ledger(
                ledger.Ledger_Id,
                ledger.Transaction_Type,
                ledger.Amount);
        }
    }
}
