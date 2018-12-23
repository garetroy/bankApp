using bankLedger.Models;
using bankLedger.Web.App_Start;
using Moq;
using NUnit.Framework;
using Smocks;
using System;
namespace bankLedger.Web.Tests.ControllerTests
{
    [TestFixture]
    public class LoginControllerTest
    {
        [Test]
        public void Login()
        {
            var service = new Mock<IBankLedgerService>();
            var accountService = new Mock<IAccountService>();
            service.SetupGet(x => x.AccountService).Returns(accountService.Object);


            var result = Smock.Run(context =>
            {

                return null;
            });
        }
    }
}
