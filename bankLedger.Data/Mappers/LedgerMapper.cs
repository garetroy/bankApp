using System;
using bankLedger.Data.DbObject;
using bankLedger.Models;

namespace bankLedger.Data.Mappers
{
    internal sealed class LedgerMapper
    {
        public Ledger Map(DbLedger ledger)
        {
            return new Ledger(
            ledger.Transaction_Type,
            ledger.Amount);
        }
    }
}
