using bankLedger.Data.Services;
using bankLedger.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace bankLedger.Data.Tests
{
    [TestFixture]
    public class AccountServiceTest
    {
        [Test]
        public void CreateAccountTests()
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
    }
}
