using bankLedger.Models;
using bankLedger.Web.Controllers;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using System;
using System.Web;
using System.Web.Mvc;

//To supress warning from MvcContrib
#pragma warning disable CS1701 // Assuming assembly reference matches identity
namespace bankLedger.Web.Tests.ControllerTests
{
    [TestFixture]
    public partial class LoginControllerTest
    {
        [SetUp]
        public void Init()
        {
            Service = new Mock<IBankLedgerService>();
            AccountService = new Mock<IAccountService>();
            var session = new FakeSessionState();
            Service.SetupGet(x => x.AccountService).Returns(AccountService.Object);
            Controller = new LoginController(Service.Object);
        }

        [Test]
        public void Login()
        {
            Account account = null;

            //Verify Non-Signed in action to login
            var result = Controller.Login();
            result.AssertViewRendered().ForView("Login");

            //Verify Redirect to account info if already signed in
            account = new Account("a", "a", "a", null);
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var signedInResult = Controller.Login();
            signedInResult.AssertActionRedirect().ToController("Account")
                .ToAction("AccountInfo");
        }

        [Test]
        public void AttemptLogin()
        {
            Account account = null;

            //data = false, not signed in
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var dto = new LoginDto
            {
                UserName = "a",
                DecryptedPassword = "a"
            };
            var result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = true });

            //Data is false, bad signin credentials
            account = new Account("a", "a", "a", null);
            AccountService.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<HttpSessionStateBase>())).Returns((Account)null);
            result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = false });

            //Data is true, everything worked correctly
            AccountService.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>(),
                Session)).Returns(account);
            result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = true });
        }

        [Test]
        public void CreateAccount()
        {
            //Automatically go to account info if already signed in
            Account account = new Account("a", "a", "a", null);
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var result = Controller.CreateAccount();
            result.AssertActionRedirect().ToController("Account").ToAction("AccountInfo");

            //Returnes proper view and model
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns((Account)null);
            result = Controller.CreateAccount();
            result.AssertViewRendered().ForView("CreateAccount")
                .WithViewData<AttemptCreateUserViewModel>();
        }

        [Test]
        public void CreateAccountSubmit()
        {
            var dto = new CreateUserDto
            {
                UserName = "",
                Password = ""
            };

            //Bad Request, an account ledger wasnt created
            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns((Account)null);
            var result = Controller.CreateAccountSubmit(dto);
            var code = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
            Assert.AreEqual(400, code);

            //Properly created acconut, return Ok() (200)
            AccountService.Setup(x => x.CreateAccount(It.IsAny<string>(),
            It.IsAny<string>())).Returns(new Account("a", "a", null, null));
            result = Controller.CreateAccountSubmit(dto);
            code = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
            Assert.AreEqual(200, code);
        }

        [Test]
        public void Logout()
        {
           Controller.Logout().AssertActionRedirect().ToController("Login")
                .ToAction("Login");
        }

        private ActionResult TestBadSignin(Func<ActionResult> func)
        {
            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns((Account)null);
            var result = func();

            //If Forbidden then check for that (for posts)
            if (result.GetType() == typeof(HttpStatusCodeResult))
            {
                var code = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
                Assert.AreEqual(403, code);
                return result;
            }

            //Else check redirect (for gets)
            result.AssertActionRedirect().ToController("Login")
                .ToAction("Login");
            return result;
        }

        private Mock<IBankLedgerService> Service { get; set; }
        private Mock<IAccountService> AccountService { get; set; }
        private FakeSessionState Session { get; set; }
        private LoginController Controller { get; set; }
    }
}
#pragma warning restore CS1701 // Assuming assembly reference matches identity
