using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bankLedger.Models;
using bankLedger.Web.App_Start;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;

namespace bankLedger.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IBankLedgerService service) : base(service) { }

        [HttpGet]
        public ActionResult AccountLedger()
        {
            var account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                RedirectToAction("Login", "Login");


            var ledgers = BankLedgerService.LedgerService.GetAllLedgers(account);

            var model = new AccountLedgerViewModel
            {
                Ledgers = ledgers?.Select(x => new LedgerViewModel
                {
                    LedgerId = x.LedgerId,
                    TransactionType = x.TransactionType,
                    Amount = x.Amount
                }).ToList() ?? new List<LedgerViewModel>()
            };

            return BaseView("AccountLedger", model);
        }

        [HttpPost]
        public ActionResult CreateAccountLedger(AccountLedgerDto dto)
        {
            var account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
               return BadRequest("Account not authorized");

            var ledger = new Ledger(0, dto.TransactionType, dto.Amount);

            var newLedger = BankLedgerService.LedgerService.CreateLedger(account, ledger);
            //If there was an error, then it was not successful.
            if (newLedger == null)
                return BadRequest("Could not create Ledger");

            var model = new LedgerViewModel
            {
                LedgerId = newLedger.LedgerId,
                Amount = newLedger.Amount,
                TransactionType = newLedger.TransactionType
            };

            return PartialView("_Ledger", model);
        }

        [HttpGet]
        public ActionResult AccountInfo()
        {
            var account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                return RedirectToAction("Login", "Login");

            var count = BankLedgerService.LedgerService.GetAllLedgers(account)?.Count ?? 0;
            var totalAmount = BankLedgerService.LedgerService.GetTotalBalance(account);

            var model = new AccountInfoViewModel
            {
                LegderCount = (ulong)count,
                TotalAmount = totalAmount,
                UserName = account.UserName
            };

            return BaseView("AccountInfo", model);
        }
    }
}
