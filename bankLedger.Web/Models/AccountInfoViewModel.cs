namespace bankLedger.Web.Models
{
    public class AccountInfoViewModel
    {
        public decimal LegderCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string AccountName { get; set; }
        public decimal WithdrawlCount { get; set; }
        public decimal DepositCount { get; set; }
        public decimal WithdrawlRatio { get; set; }
        public decimal DepositRatio { get; set; }
    }
}