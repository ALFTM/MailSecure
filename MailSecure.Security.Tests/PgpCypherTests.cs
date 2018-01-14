using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure.Security.Tests
{
    [TestClass]
    public class PgpCypherTests
    {
        private static string KEY_LOGIN = "login";
        private static string KEY_PASS = "password";
        private static string KEY_PATH = "c:\\temp";

        [TestMethod]
        public void ShouldBeEqualsAfterPgpEncryption()
        {
            PgpKeyGen.generateKeys(KEY_LOGIN, KEY_PASS, KEY_PATH);
            PgpEncryptionKeys keys = new PgpEncryptionKeys(KEY_PATH + "\\pubring.gpg",
                KEY_PATH + "\\secring.gpg", KEY_PASS);
            PgpCypher cypher = new PgpCypher(keys);
            byte[] data = BytesGenerator.getInstance().generateRandom(2048);
            byte[] encodedData;
            byte[] decodedData = null;

            encodedData = cypher.Encrypt(data);
            decodedData = cypher.Decrypt(encodedData);

            Console.WriteLine(BitConverter.ToString(data));
            Console.WriteLine(BitConverter.ToString(encodedData));
            Console.WriteLine(BitConverter.ToString(decodedData));

            Assert.AreEqual(BitConverter.ToString(data), BitConverter.ToString(decodedData));
        }
    }
}
