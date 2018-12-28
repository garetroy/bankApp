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
    public class LoginController : BaseController
    {
        public LoginController(IBankLedgerService service) : base(service) { }

        [HttpGet]
        public ActionResult Login()
        {
            if (BankLedgerService.AccountService.IsSignedIn(Session) != null)
                return RedirectToAction("AccountInfo", "Account");

            var model = new LoginViewModel();

            return BaseView("Login", model);
        }

        [HttpPost]
        public ActionResult AttemptLogin(LoginDto dto)
        {
            if(BankLedgerService.AccountService.IsSignedIn(Session) != null)
                return Json(new { data = false });


            var account = BankLedgerService.AccountService.SignIn(dto.UserName,
                                       dto.DecryptedPassword, Session);
            if (account != null)
            {
                //Login was good, redirect to accountinfo
                return Json(new { data = true });
            }

            //Login was bad, dont redirect
            return Json(new { data = false });
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            if (BankLedgerService.AccountService.IsSignedIn(Session) != null)
                return RedirectToAction("AccountInfo", "Account");

            var model = new AttemptCreateUserViewModel();

            return BaseView("CreateAccount", model);
        }

        [HttpPost]
        public ActionResult CreateAccountSubmit(CreateUserDto dto)
        {
            var newUser = BankLedgerService.AccountService.CreateAccount(dto.UserName,
                                                            dto.Password);

            if(newUser == null)
            {
                //Don't redirect, creation was a failure
                return BadRequest("Could not create account.");
            }

            var model = new LoginViewModel
            {
                SuccessfulCreate = true
            };
            //Redirect to login
            return Ok();

        }

        [HttpGet]
        public ActionResult Logout()
        {
            BankLedgerService.AccountService.SignOut(Session);
            return Redirect("/");
        }

        public readonly StructureMapResolver m_resolver;
    }
}
