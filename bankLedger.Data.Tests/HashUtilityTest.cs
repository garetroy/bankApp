using NUnit.Framework;
using System;

namespace bankLedger.Data.Tests
{
    [TestFixture]
    public class HashUtilityTest
    {
        [Test]
        public void HashUtilityTests()
        {
            var password = "Wowza";
            var badGuess = "thisshouldntwork";
            var badGuess2 = "wowza";

            var generatedHash = HashUtility.ComputeHash(password);
            var badSalt = generatedHash.Item2 + "k";
            Assert.Throws<FormatException>(() => HashUtility.VerifyHash(password, generatedHash.Item1, badSalt));

            Assert.IsFalse(HashUtility.VerifyHash(badGuess, generatedHash.Item1, generatedHash.Item2));
            Assert.IsFalse(HashUtility.VerifyHash(badGuess2, generatedHash.Item1, generatedHash.Item2));
            Assert.IsTrue(HashUtility.VerifyHash(password, generatedHash.Item1, generatedHash.Item2));
        }
    }
}