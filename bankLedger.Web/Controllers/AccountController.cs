using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Web.App_Start;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;

namespace bankLedger.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(StructureMapResolver resolver) : base(resolver)
        {
            m_resolver = resolver;
        }

        [HttpGet]
        public ActionResult AccountLedger()
        {
            var account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                RedirectToAction("Login", "Login");


            var ledgers = BankLedgerService.LedgerService.GetAllLedgers(account);

            var model = new AccountLedgerViewModel
            {
                Ledgers = ledgers.Select(x => new LedgerViewModel
                {
                    LedgerId = x.LedgerId,
                    TransactionType = x.TransactionType,
                    Amount = x.Amount
                }).ToList()
            };

            return BaseView("AccountLedger", model);
        }

        [HttpPost]
        public ActionResult CreateAccountLedger(AccountLedgerDto dto)
        {
            return null;
        }

        [HttpGet]
        public ActionResult AccountInfo()
        {
            return BaseView("AccountInfo");
        }

        public readonly StructureMapResolver m_resolver;
    }
}
