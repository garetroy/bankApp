using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Web.App_Start;

namespace bankLedger.Web.Controllers
{
    public class AccountController : Controller
    {
        //TODO: Need to set base controller.
        public AccountController(StructureMapResolver resolver)
        {
            m_resolver = resolver;
        }

        public ActionResult AccountLedger()
        {
            return View();
        }

        public ActionResult AccountInfo()
        {
            return View();
        }

        public readonly StructureMapResolver m_resolver;
    }
}
