using StructureMap;
using bankLedger.Web.DependencyInjection;

namespace bankLedger.Web.App_Start
{
    public class TypeConfig
    {
        public static IContainer RegisterTypes()
        {
            return new Container(x =>
            {
                x.AddRegistry(new BankLedgerRegistry());
            });
        }
    }
}
