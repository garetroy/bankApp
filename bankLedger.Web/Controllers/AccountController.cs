using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bankLedger.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult AccountLedger()
        {
            return View();
        }

        public ActionResult AccountInfo()
        {
            return View();
        }
    }
}
