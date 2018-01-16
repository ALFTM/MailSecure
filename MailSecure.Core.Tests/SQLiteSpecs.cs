using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MailSecure.Core.Tests
{
    [TestClass]
    [TestCategory("SQLite")]
    public class SQLiteSpecs
    {
        private const string TEST_NAME = "Toto";
        private const string TEST_HASH = "hash";
        private const string TEST_PASS = "pass";
        private SQLiteHelper helper = new SQLiteHelper();

        [TestMethod]
        public void Can_Create_Database()
        {
            helper.CreateFile();
            Assert.IsTrue(File.Exists(AppConst.APP_DATABASE_NAME));

        }
        [TestMethod]
        public void Can_Open_Database()
        {
            helper.OpenDataBase();
        }
        [TestMethod]
        public void Can_Initialize_Tables()
        {
            helper.InitTables();
        }
        [TestMethod]
        public void Can_Add_User()
        {
            byte[] hash;
            byte[] pass;
            hash = Encoding.ASCII.GetBytes(TEST_HASH);
            pass = Encoding.ASCII.GetBytes(TEST_PASS);
            helper.AddUser(TEST_NAME, hash, pass);
        }

        [TestMethod]
        public void Can_Read_User()
        {
            DataBaseUser user = helper.GetUser(TEST_NAME);

            Assert.AreEqual(TEST_NAME, user.Name);
            Assert.AreEqual(TEST_HASH, Encoding.ASCII.GetString(user.Hash));
            Assert.AreEqual(TEST_PASS, Encoding.ASCII.GetString(user.Pass));
        }

        [TestMethod]
        public void Can_Read_All()
        {
            List<DataBaseUser> users = helper.ReadAll();
            DataBaseUser toto = users[0];

            Assert.AreEqual(TEST_NAME, toto.Name);
        }

        [TestMethod]
        public void Can_Check_If_User_Exist()
        {
            bool checkToto = helper.CheckIfUserExist(TEST_NAME);
            // bool checkOther = helper.CheckIfUserExist("Test");

            Assert.IsTrue(checkToto);
            // Assert.IsFalse(checkOther);
        }

        [TestMethod]
        public void Can_Delete_Database_File()
        {
            helper.DeleteFile();
            Assert.IsFalse(File.Exists(AppConst.APP_DATABASE_NAME));
        }

    }
}
