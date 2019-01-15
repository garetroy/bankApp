using bankLedger.Data;
using bankLedger.Data.Services;
using bankLedger.Models;
using StructureMap;
using StructureMap.Web;

namespace bankLedger.Web.DependencyInjection
{
    public class BankLedgerRegistry : Registry
    {
        public BankLedgerRegistry()
        {
            For<DataBase>().Singleton().Use(c => new DataBase());
            For<IBankLedgerService>().HttpContextScoped().Use(c => new BankLedgerService(c.GetInstance<DataBase>().DB));
        }
    }
}