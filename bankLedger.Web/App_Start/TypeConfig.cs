using System;
using StructureMap;
using StructureMap.Web;
using bankLedger.Models;
using bankLedger.Data.Services;
using System.Collections.Generic;

namespace bankLedger.Web.App_Start
{
    public class TypeConfig
    {
        public static IContainer RegisterTypes()
        {
            return new Container(x =>
            {
                x.For<IBankLedgerService>().HttpContextScoped().Use(c => new BankLedgerService(DataBase));
            });
        }

        //Because we are not using persistant storage
        public static Dictionary<string, object> DataBase = new Dictionary<string, object>();
    }
}
