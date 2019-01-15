namespace bankLedger.Data.DbObject
{
    internal sealed class DbLedger
    {
        public ulong Ledger_Id { get; set; }
        public int Transaction_Type { get; set; }
        public decimal Amount { get; set; }
    }
}