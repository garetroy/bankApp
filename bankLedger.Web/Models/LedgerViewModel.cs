using bankLedger.Models;

namespace bankLedger.Web.Models
{
    public class LedgerViewModel
    {
        public ulong LedgerId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
