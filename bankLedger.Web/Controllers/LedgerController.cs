using bankLedger.Models;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace bankLedger.Web.Controllers
{
    public class LedgerController : BaseController
    {
        public LedgerController(IBankLedgerService service) : base(service)
        {
        }

        [HttpGet]
        public ActionResult AccountLedger()
        {
            Account account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                return RedirectToAction("Login", "Login");

            ICollection<Ledger> ledgers = BankLedgerService.LedgerService.GetAllLedgers(account);

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
            Account account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                return Forbidden("Account not authorized");

            var ledger = new Ledger(0, dto.TransactionType, dto.Amount);

            Ledger newLedger = BankLedgerService.LedgerService.CreateLedger(account, ledger);
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
            Account account = BankLedgerService.AccountService.IsSignedIn(Session);

            if (account == null)
                return RedirectToAction("Login", "Login");

            ICollection<Ledger> ledgers = BankLedgerService.LedgerService.GetAllLedgers(account);
            decimal count = ledgers?.Count ?? 0;
            decimal withdrawlCount = ledgers != null ? ledgers.Where(x => x.TransactionType == TransactionType.Withdrawl).Count() : 0;
            decimal depositCount = count - withdrawlCount;
            decimal totalAmount = BankLedgerService.LedgerService.GetTotalBalance(account);

            decimal withdrawlRatio = 100;
            decimal depositRatio = 100;
            if (count != 0)
            {
                withdrawlRatio = (withdrawlCount / count) * 100;
                depositRatio = (depositCount / count) * 100;
            }

            var model = new AccountInfoViewModel
            {
                LegderCount = (ulong)count,
                TotalAmount = totalAmount,
                AccountName = account.UserName,
                WithdrawlCount = withdrawlCount,
                WithdrawlRatio = withdrawlRatio,
                DepositCount = depositCount,
                DepositRatio = depositRatio
            };

            return BaseView("AccountInfo", model);
        }
    }
}