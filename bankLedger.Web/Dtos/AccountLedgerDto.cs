using System;
namespace bankLedger.Web.Dtos
{
    public class AccountLedgerDto
    {
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
