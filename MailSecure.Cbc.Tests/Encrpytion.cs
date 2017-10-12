using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MailSecure.Cbc.Tests
{
    [TestClass]
    [TestCategory("CBC")]
    public class Encrpytion
    {
        [TestMethod]
        [TestCategory("CBC")]
        public void Should_Be_Equals_After_Encryption()
        {
            BytesGenerator generator = BytesGenerator.getInstance();
            CbcCypher cypher = new CbcCypher(128);
            byte[] data = generator.generateRandom(2048);
            byte[] key = generator.generateRandom(128);
            byte[] encodedData = cypher.encrypt(data, key);
            byte[] decodedData = cypher.decrypt(encodedData, key);

            string dataString = BitConverter.ToString(data);
            string decodedDataString = BitConverter.ToString(decodedData);


            Assert.AreEqual(dataString, decodedDataString);
           
        }
    }
}
