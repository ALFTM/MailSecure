using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSecure.Security.Tests
{
    [TestClass]
    [TestCategory("Encryption")]
    public class EncryptionTests
    {
        [TestMethod]
        public void EncryptString()
        {
            // Arrange
            var exceptedResult = "i'm a test";
            var password = "password";

            // Act
            var res = Encryption.Encrypt(exceptedResult, password);
            Assert.AreNotEqual(exceptedResult, res);
            res = Encryption.Decrypt(res, password);

            // Assert
            Assert.AreEqual(exceptedResult, res);
        }

        [TestMethod]
        public void EncryptTwoStringAreDifferent()
        {
            // Arrange
            var exceptedResult = "i'm a test";
            var password = "password";

            // Act
            var firstRes = Encryption.Encrypt(exceptedResult, password);
            var secondRes = Encryption.Encrypt(exceptedResult, password);

            // Assert
            Assert.AreNotEqual(firstRes, secondRes);
        }
    }
}
