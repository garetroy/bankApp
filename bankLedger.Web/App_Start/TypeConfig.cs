using System;
using StructureMap;
using StructureMap.Web;
using bankLedger.Models;
using bankLedger.Data.Services;
using System.Collections.Generic;
using bankLedger.Data;

namespace bankLedger.Web.App_Start
{
    public class TypeConfig
    {
        public static IContainer RegisterTypes()
        {
            return new Container(x =>
            {
                x.For<DataBase>().Singleton().Use(c => new DataBase());
                x.For<IBankLedgerService>().HttpContextScoped().Use(c => new BankLedgerService(c.GetInstance<DataBase>().DB));
            });
        }
    }
}
