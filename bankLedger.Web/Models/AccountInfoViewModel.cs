using System;
namespace bankLedger.Web.Models
{
    public class AccountInfoViewModel
    {
        public ulong LegderCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserName { get; set; }
    }
}
