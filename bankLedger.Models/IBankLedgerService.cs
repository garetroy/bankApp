using System.Collections.Generic;

namespace bankLedger.Models
{
    public interface IBankLedgerService
    {
        IAccountService AccountService { get; }
        ILedgerService LedgerService { get; }
        IDictionary<string, object> DataBase { get; set; }
    }
}