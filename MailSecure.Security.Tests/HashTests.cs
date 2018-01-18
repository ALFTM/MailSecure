using System.Collections.Generic;
using MailSecure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSecure.Security.Tests
{
    [TestClass]
    [TestCategory("HASH")]
    public class HashTests
    {
        [TestMethod()]
        public void InitBase()
        {
            SQLiteHelper helper = new SQLiteHelper();
            try {
                helper.CreateFile();
            }
            finally {
                helper.InitTables();
            }
        }
        [TestMethod]
        public void AddUSer()
        {
            UserManager userManager = new UserManager();
            string pass = "Password";
            bool result = userManager.AddUserInDataBaseFromString("Tata", pass);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUser()
        {
            SQLiteHelper helper = new SQLiteHelper();
            List<DataBaseUser> users = helper.ReadAll();
            DataBaseUser tata = users[0];

            Assert.AreEqual("Tata", tata.Name);
        }
    }
}
