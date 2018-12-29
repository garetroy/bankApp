using bankLedger.Models;
using bankLedger.Web.Controllers;
using bankLedger.Web.Dtos;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
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

            //Verify Non-Signed in action
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var nonSignedInResult = Controller.Login();
            nonSignedInResult.AssertViewRendered().ForView("Login");

            //Verify Redirect
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

            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var dto = new LoginDto
            {
                UserName = "a",
                DecryptedPassword = "a"
            };
            var result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = true });

            account = new Account("a", "a", "a", null);
            AccountService.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<HttpSessionStateBase>())).Returns((Account)null);
            result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = false });

            AccountService.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>(), 
                Session)).Returns(account);
            result = Controller.AttemptLogin(dto);
            result.AssertResultIs<ActionResult>().Equals(new { data = true });
        }

        [Test]
        public void CreateAccount()
        {
            Account account = new Account("a", "a", "a", null);
            AccountService.Setup(x => x.IsSignedIn(Session)).Returns(account);
            var result = Controller.CreateAccount();
            result.AssertActionRedirect().ToController("Account").ToAction("AccountInfo");

            AccountService.Setup(x => x.IsSignedIn(Session)).Returns((Account)null);
            result = Controller.CreateAccount();
            result.AssertViewRendered().ForView("CreateAccount");
        }

        [Test]
        public void CreateAccountSubmit()
        {
            AccountService.Setup(x => x.CreateAccount(It.IsAny<string>(),
            It.IsAny<string>())).Returns((Account)null);

            var dto = new CreateUserDto
            {
                UserName = "",
                Password = ""
            };

            var result = Controller.CreateAccountSubmit(dto);
            var statusCode = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
            Assert.AreEqual(400, statusCode);

            AccountService.Setup(x => x.CreateAccount(It.IsAny<string>(),
            It.IsAny<string>())).Returns(new Account("a", "a", null, null));
            result = Controller.CreateAccountSubmit(dto);
            statusCode = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
            Assert.AreEqual(200, statusCode);
        }

        [Test]
        public void Logout()
        {
           Controller.Logout().AssertActionRedirect().ToController("Login")
                .ToAction("Login");
        }

        private Mock<IBankLedgerService> Service { get; set; }
        private Mock<IAccountService> AccountService { get; set; }
        private FakeSessionState Session { get; set; }
        private LoginController Controller { get; set; }
    }
}
#pragma warning restore CS1701 // Assuming assembly reference matches identity
