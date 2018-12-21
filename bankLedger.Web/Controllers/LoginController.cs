using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Web.App_Start;

namespace bankLedger.Web.Controllers
{
    public class LoginController : Controller
    {
        //TODO: Need to set base controller
        public LoginController(StructureMapResolver resolver)
        {
            m_resolver = resolver;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public readonly StructureMapResolver m_resolver;
    }
}
