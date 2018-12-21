using System;
namespace bankLedger.Data.DbObject
{
    internal sealed class DbLedger
    {
        public int Transaction_Type { get; set; }
        public decimal Amount { get; set; }
    }
}
