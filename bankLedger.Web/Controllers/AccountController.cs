using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Web.App_Start;

namespace bankLedger.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(StructureMapResolver resolver) : base(resolver)
        {
            m_resolver = resolver;
        }

        public ActionResult AccountLedger()
        {
            return BaseView("AccountLedger");
        }

        public ActionResult AccountInfo()
        {
            return View();
        }

        public readonly StructureMapResolver m_resolver;
    }
}
