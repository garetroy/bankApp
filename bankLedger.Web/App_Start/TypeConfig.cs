using System;
using StructureMap;
using StructureMap.Web;
using bankLedger.Models;
using bankLedger.Data.Services;

namespace bankLedger.Web.App_Start
{
    public class TypeConfig
    {
        public static IContainer RegisterTypes()
        {
            return new Container(x =>
            {
                x.For<IBankLedgerService>().HttpContextScoped().Use(c => new BankLedgerService());
            });
        }
    }
}
