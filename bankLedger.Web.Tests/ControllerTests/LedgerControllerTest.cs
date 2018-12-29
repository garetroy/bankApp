using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using bankLedger.Models;
using bankLedger.Web.Controllers;
using bankLedger.Web.Dtos;
using bankLedger.Web.Models;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;


//To supress warning from MvcContrib
#pragma warning disable CS1701 // Assuming assembly reference matches identity
namespace bankLedger.Web.Tests.ControllerTests
{
    [TestFixture]
    public class AccountControllerTest
    {

        [SetUp]
        public void Init()
        {
            Service = new Mock<IBankLedgerService>();
            AccountService = new Mock<IAccountService>();
            LedgerService = new Mock<ILedgerService>();
            Service.SetupGet(x => x.LedgerService).Returns(LedgerService.Object);
            Service.SetupGet(x => x.AccountService).Returns(AccountService.Object);
            Controller = new LedgerController(Service.Object);
        }

        [Test]
        public void AccountLedgerTest()
        {
            //Redirect
            TestBadSignin(Controller.AccountLedger);

            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns(new Account("", "", "", null));

            var results = new List<Ledger>
            {
                new Ledger(0,TransactionType.Deposit,0),
                new Ledger(1,TransactionType.Deposit,0),
                new Ledger(2,TransactionType.Withdrawl,0),
                new Ledger(3,TransactionType.Withdrawl,0),
                new Ledger(4,TransactionType.Deposit,0),
                new Ledger(5,TransactionType.Withdrawl,0),
            };

            //Verify it can handle null
            LedgerService.Setup(x => x.GetAllLedgers(It.IsAny<Account>()))
                .Returns((List<Ledger>)null);
            var result = Controller.AccountLedger();
            result.AssertViewRendered().ForView("AccountLedger");
            var model = result.AssertViewRendered().WithViewData<AccountLedgerViewModel>();
            Assert.IsEmpty(model.Ledgers);

            var viewModels = new List<LedgerViewModel>();
            LedgerService.Setup(x => x.GetAllLedgers(It.IsAny<Account>()))
                .Returns(results);
            foreach( var ledger in results)
            {
                viewModels.Add(new LedgerViewModel
                {
                    LedgerId = ledger.LedgerId,
                    TransactionType = ledger.TransactionType,
                    Amount = ledger.Amount
                });
            }

            //Returns AccountLedger view. Models should match.
            result = Controller.AccountLedger();
            result.AssertViewRendered().ForView("AccountLedger");
            model = result.AssertViewRendered().WithViewData<AccountLedgerViewModel>();
            Assert.AreEqual(viewModels.Count, model.Ledgers.Count);
            for (int i = 0; i < viewModels.Count; i++)
            {
                var areEqual = viewModels[i].Amount == model.Ledgers[i].Amount;
                areEqual &= viewModels[i].TransactionType == model.Ledgers[i].TransactionType;
                areEqual &= viewModels[i].LedgerId == model.Ledgers[i].LedgerId;
                Assert.IsTrue(areEqual);
            }
        }

        [Test]
        public void CreateAccountLedgerTest()
        {
            //Forbidden
            TestBadSignin(() => Controller.CreateAccountLedger(null));

            var dto = new AccountLedgerDto
            {
                Amount = 1,
                TransactionType = 1
            };

            //Bad Request, an account ledger wasnt created
            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns(new Account("","","",null));
            LedgerService.Setup(x => x.CreateLedger(It.IsAny<Account>(),
                It.IsAny<Ledger>())).Returns((Ledger)null);
            var result = Controller.CreateAccountLedger(dto);
            var code = result.AssertResultIs<HttpStatusCodeResult>().StatusCode;
            Assert.AreEqual(400, code);

            //Should create correct ledgerview model with partialview
            var ledger = new Ledger(1, dto.TransactionType, dto.Amount);
            LedgerService.Setup(x => x.CreateLedger(It.IsAny<Account>(),
                It.IsAny<Ledger>())).Returns(ledger);

            result = Controller.CreateAccountLedger(dto);
            result.AssertPartialViewRendered().ForView("_Ledger");
            var model = result.AssertPartialViewRendered().WithViewData<LedgerViewModel>();
            Assert.AreEqual(ledger.LedgerId, model.LedgerId);
            Assert.AreEqual(ledger.TransactionType, model.TransactionType);
            Assert.AreEqual(ledger.Amount, model.Amount);
        }

        [Test]
        public void AccountIntoTest()
        {
            //Redirect
            TestBadSignin(Controller.AccountInfo);

            //Returns a model with 0 count, 10 totalcount, "name" accountname
            LedgerService.Setup(x => x.GetAllLedgers(It.IsAny<Account>()))
                .Returns((List<Ledger>)null);
            LedgerService.Setup(x => x.GetTotalBalance(It.IsAny<Account>()))
                .Returns(10);
            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns(new Account("name","","",null));

            var result = Controller.AccountInfo();
            result.AssertViewRendered().ForView("AccountInfo");
            var model = result.AssertViewRendered().WithViewData<AccountInfoViewModel>();
            Assert.AreEqual(0, model.LegderCount);
            Assert.AreEqual(10, model.TotalAmount);
            Assert.AreEqual("name", model.AccountName);
        }

        private ActionResult TestBadSignin(Func<ActionResult> func)
        {
            AccountService.Setup(x => x.IsSignedIn(It.IsAny<HttpSessionStateBase>()))
                .Returns((Account)null);
            var result = func();

            //If Forbidden then check for that (for posts)
            if(result.GetType() == typeof(HttpStatusCodeResult))
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
        private Mock<ILedgerService> LedgerService { get; set; }
        private Mock<IAccountService> AccountService { get; set; }
        private LedgerController Controller { get; set; }
    }
}
#pragma warning restore CS1701 // Assuming assembly reference matches identity
