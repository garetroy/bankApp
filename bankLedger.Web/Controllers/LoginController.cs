using bankLedger.Models;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;
using System.Web.Mvc;

namespace bankLedger.Web.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IBankLedgerService service) : base(service)
        {
        }

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
            if (BankLedgerService.AccountService.IsSignedIn(Session) != null)
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

            if (newUser == null)
            {
                //Don't redirect, creation was a failure
                return BadRequest("Could not create account.");
            }

            //Redirect to login
            return Ok();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            BankLedgerService.AccountService.SignOut(Session);
            return RedirectToAction("Login", "Login");
        }
    }
}