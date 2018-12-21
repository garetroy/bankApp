using System;
namespace bankLedger.Models
{
    public interface IBankLedgerService
    {
        IAccountService AccountService { get; }
        ILedgerService LedgerService { get; }
        IHashService HashService { get; }
    }
}
