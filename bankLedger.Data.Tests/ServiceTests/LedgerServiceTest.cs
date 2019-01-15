using bankLedger.Data.Services;
using bankLedger.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bankLedger.Data.Test.ServiceTests
{
    [TestFixture]
    public class LedgerServiceTest
    {
        [Test]
        public void CreateLedgers()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var ledgerService = new LedgerService(service.Object);

            var badAccount = new Account(null, "password", "", null);
            var account = new Account("testing", "password", "", null);
            var depLedger = new Ledger(0, TransactionType.Deposit, 10);
            var withLedger = new Ledger(0, TransactionType.Withdrawl, 10);
            Assert.Throws<ArgumentException>(() => ledgerService.CreateLedger(null, null));
            Assert.Throws<ArgumentException>(() => ledgerService.CreateLedger(badAccount, null));
            Assert.Throws<ArgumentException>(() => ledgerService.CreateLedger(account, null));

            var ledgerKey = $"{account.UserName.ToLower()}_Ledger_{1}";
            ledgerService.BankLedgerService.DataBase[ledgerKey] = null;
            Assert.IsNull(ledgerService.CreateLedger(account, depLedger));

            var ledgerCountKey = $"{account.UserName.ToLower()}_ledgerCount";

            ledgerService.BankLedgerService.DataBase.Remove(ledgerKey);
            Assert.IsNotNull(ledgerService.CreateLedger(account, depLedger));
            Assert.IsNotNull(ledgerService.BankLedgerService.DataBase[ledgerCountKey]);
            Assert.AreEqual(1, (ulong)ledgerService.BankLedgerService.DataBase[ledgerCountKey]);

            Assert.IsNotNull(ledgerService.CreateLedger(account, withLedger));
            Assert.IsNotNull(ledgerService.BankLedgerService.DataBase[ledgerCountKey]);
            Assert.AreEqual(2, (ulong)ledgerService.BankLedgerService.DataBase[ledgerCountKey]);
        }

        [Test]
        public void GetLedgers()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var ledgerService = new LedgerService(service.Object);
            var account = new Account("testing", "password", "", null);
            var badAccount = new Account(null, "password", "", null);
            var depLedger = new Ledger(0, TransactionType.Deposit, 10);
            var withLedger = new Ledger(0, TransactionType.Withdrawl, 10);

            Assert.Throws<ArgumentException>(() => ledgerService.GetAllLedgers(null));
            Assert.Throws<ArgumentException>(() => ledgerService.GetAllLedgers(badAccount));

            Assert.IsNull(ledgerService.GetAllLedgers(account));

            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);
            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);
            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);

            Assert.AreEqual(6, ledgerService.GetAllLedgers(account).Count);
            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);
            Assert.AreEqual(8, ledgerService.GetAllLedgers(account).Count);
            Assert.IsTrue(ledgerService.GetAllLedgers(account).Any(x => x.LedgerId == 8));
            Assert.AreEqual(4, ledgerService.GetAllLedgers(account)
                .Count(x => x.TransactionType == TransactionType.Deposit));
            Assert.AreEqual(4, ledgerService.GetAllLedgers(account)
                .Count(x => x.TransactionType == TransactionType.Withdrawl));
        }

        [Test]
        public void GetBalance()
        {
            var dictionary = new Dictionary<string, object>();
            var service = new Mock<IBankLedgerService>();
            service.SetupGet(x => x.DataBase).Returns(dictionary);
            var ledgerService = new LedgerService(service.Object);
            var account = new Account("testing", "password", "", null);
            var badAccount = new Account(null, "password", "", null);
            var depLedger = new Ledger(0, TransactionType.Deposit, 10);
            var withLedger = new Ledger(0, TransactionType.Withdrawl, 10);

            Assert.Throws<ArgumentException>(() => ledgerService.GetTotalBalance(null));
            Assert.Throws<ArgumentException>(() => ledgerService.GetTotalBalance(badAccount));

            Assert.AreEqual(0, ledgerService.GetTotalBalance(account));

            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);
            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);
            ledgerService.CreateLedger(account, depLedger);
            ledgerService.CreateLedger(account, withLedger);

            Assert.AreEqual(0, ledgerService.GetTotalBalance(account));

            ledgerService.CreateLedger(account, depLedger);
            Assert.AreEqual(10, ledgerService.GetTotalBalance(account));

            ledgerService.CreateLedger(account, withLedger);
            ledgerService.CreateLedger(account, withLedger);
            Assert.AreEqual(-10, ledgerService.GetTotalBalance(account));

            for (int i = 0; i < 11; i++)
                ledgerService.CreateLedger(account, depLedger);

            Assert.AreEqual(100, ledgerService.GetTotalBalance(account));

            for (int i = 0; i < 11; i++)
                ledgerService.CreateLedger(account, withLedger);

            Assert.AreEqual(-10, ledgerService.GetTotalBalance(account));
        }
    }
}