using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Models;
using bankLedger.Web.App_Start;

namespace bankLedger.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(StructureMapResolver resolver)
        {
            BankLedgerService = StructureMapResolver.GetService<IBankLedgerService>();
        }

        protected IBankLedgerService BankLedgerService { get; }
    }
}
