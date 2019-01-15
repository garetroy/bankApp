using bankLedger.Data.Services;
using bankLedger.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;

namespace bankLedger.Data.Test.ServiceTests
{
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void CreateTests()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var accountService = new AccountService(service.Object);

            dictionary["USER_fail"] = null;
            var failure = accountService.CreateAccount("FAIL", "test");
            Assert.IsNull(failure);

            var success = accountService.CreateAccount("Success", "test");
            Assert.AreSame("Success", success?.UserName);

            //Test that you cannot have the same useraccount with the
            // same name but different capitalization
            var failure2 = accountService.CreateAccount("succEss", "test");
            Assert.IsNull(failure2);
        }

        [Test]
        public void SignInTests()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var accountService = new AccountService(service.Object);
            var session = new FakeSessionState();

            var account = accountService.CreateAccount("testing", "login");

            //BadLogin
            Assert.IsNull(session["CURRENTUSER"]);
            Assert.IsNull(accountService.SignIn("testing", "badPassword", session));
            Assert.IsNull(session["CURRENTUSER"]);

            //Correct Login
            Assert.IsNotNull(accountService.SignIn("testing", "login", session));
            Assert.IsNotNull(session["CURRENTUSER"]);

            //Already Logged in
            Assert.IsNull(accountService.SignIn("testing", "login", session));

            //Case Insensitive username
            session["CURRENTUSER"] = null;
            Assert.IsNotNull(accountService.SignIn("testing", "login", session));
        }

        [Test]
        public void SignedInTests()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var accountService = new AccountService(service.Object);
            var session = new FakeSessionState();

            var account = accountService.CreateAccount("testing", "login");

            Assert.IsNull(accountService.IsSignedIn(session));
            session["CURRENTUSER"] = account;
            Assert.IsNotNull(accountService.IsSignedIn(session));
        }

        [Test]
        public void SignOutTests()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var accountService = new AccountService(service.Object);
            var session = new FakeSessionState();

            var account = accountService.CreateAccount("testing", "logout");
            session["CURRENTUSER"] = account;

            //Don't log out an account that wasnt already logged in
            var account2 = accountService.CreateAccount("testing2", "logout");
            session["CURRENTUSER"] = account2;
            accountService.SignOut(session);
            Assert.IsNull(session["CURRENTUSER"]);

            accountService.SignOut(session);
            Assert.IsNull(session["CURRENTUSER"]);
        }

        private class FakeSessionState : HttpSessionStateBase
        {
            private Dictionary<string, object> items = new Dictionary<string, object>();

            public override object this[string name]
            {
                get { return items.ContainsKey(name) ? items[name] : null; }
                set { items[name] = value; }
            }
        }
    }
}