using System;
using StructureMap;
using StructureMap.Web;
using bankLedger.Models;
using bankLedger.Data.Services;
using System.Collections.Generic;
using bankLedger.Data;
using bankLedger.Web.DependencyInjection;

namespace bankLedger.Web.App_Start
{
    public class TypeConfig
    {
        public static IContainer RegisterTypes()
        {
            return new Container(x =>
            {
                x.AddRegistry(new AppRegister());
            });
        }
    }
}
