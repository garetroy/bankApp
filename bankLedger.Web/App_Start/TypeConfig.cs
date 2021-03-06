﻿using bankLedger.Web.DependencyInjection;
using StructureMap;

namespace bankLedger.Web.App_Start
{
    public static class TypeConfig
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