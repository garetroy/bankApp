using System;
using bankLedger.Models;
using bankLedger.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace bankLedger.Web.Tests.ControllerTests
{
    [TestFixture]
    public class AccountControllerTest
    {

        [SetUp]
        public void Init()
        {
            Service = new Mock<IBankLedgerService>();
            LedgerService = new Mock<ILedgerService>();
            Service.SetupGet(x => x.LedgerService).Returns(LedgerService.Object);
            Controller = new LedgerController(Service.Object);
        }

        private Mock<IBankLedgerService> Service { get; set; }
        private Mock<ILedgerService> LedgerService { get; set; }
        private LedgerController Controller { get; set; }
    }
}
