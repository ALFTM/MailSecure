using System;
using System.Data.SQLite;
using System.IO;

namespace MailSecure.Core
{
    public class SQLiteHelper
    {
        private readonly string connectionInfo = ("Data Source =" + AppConst.APP_DATABASE_NAME + ";Version=3;");
        private SQLiteConnection connection;
        public void CreateFile()
        {
            SQLiteConnection.CreateFile(AppConst.APP_DATABASE_NAME);
        }

        public void DeleteFile()
        {
            
            try {
                GC.Collect();
            }
            catch {
                throw new Exception("GC failed to collect");
            }
            

            try {
                SQLiteConnection.ClearAllPools();
            }
            catch {
                throw new Exception("Failed to clear all pools");
            }

            File.Delete(AppConst.APP_DATABASE_NAME);
        }

        public void FlushBase()
        {
            SQLiteConnection.ClearAllPools();
        }

        public void OpenDataBase()
        {
            connection = new SQLiteConnection(connectionInfo);
            connection.Open();

        }

        public void CloseDataBase()
        {
            connection.Close();
        }

        public int InitTables()
        {
            string sql = "create table users (name text, hash text)";

            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            int result = command.ExecuteNonQuery();

            CloseDataBase();

            return result;
        }

        public int AddUser(string name, byte[] hash)
        {
            string hashInString = System.Text.Encoding.ASCII.GetString(hash);
            string sql = "insert into users (name, hash) values ('"+ name +"', '" + hashInString + "')";

            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            int result = command.ExecuteNonQuery();

            CloseDataBase();

            return result;
        }

        public void ReadAll()
        {
            string sql = "select * from users order by name desc";

            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tHash: " + reader["hash"]);

            CloseDataBase();
        }
    }
}
