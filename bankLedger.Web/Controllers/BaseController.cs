﻿using bankLedger.Models;
using System.Web.Mvc;

namespace bankLedger.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(IBankLedgerService service)
        {
            BankLedgerService = service;
        }

        protected ActionResult BaseView(string view = null, object model = null)
        {
            return View(view, model);
        }

        protected ActionResult BadRequest(string message)
        {
            return new HttpStatusCodeResult(400);
        }

        protected ActionResult Forbidden(string logMessage = "", bool redirect = false)
        {
            if (redirect)
            {
                return RedirectToAction("Login", "Login");
            }
            return new HttpStatusCodeResult(403);
        }

        protected ActionResult Ok()
        {
            return new HttpStatusCodeResult(200);
        }

        protected IBankLedgerService BankLedgerService { get; }
    }
}