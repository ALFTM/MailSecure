using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MailSecure.Core.Tests
{
    [TestClass]
    [TestCategory("SQLite")]
    public class SQLiteSpecs
    {
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
            string test = "hash";
            hash = System.Text.Encoding.ASCII.GetBytes(test);
            helper.AddUser("Toto", hash);
        }

        public void Can_Read_User()
        {

        }
        [TestMethod]
        public void Can_Delete_Database_File()
        {
            //helper.CloseDataBase();
            helper.DeleteFile();
            Assert.IsFalse(File.Exists(AppConst.APP_DATABASE_NAME));
        }

    }
}
