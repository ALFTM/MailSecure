using System;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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
                GC.WaitForPendingFinalizers();
            }
            catch {
                throw new Exception("GC failed to collect");
            }
            

            try {
                FlushBase();
            }
            catch {
                throw new Exception("Failed to clear all pools");
            }

            bool worked = false;
            int tries = 1;
            while ((tries < 4) && (!worked)) {
                try {
                    Thread.Sleep(tries * 100);
                    if (File.Exists(AppConst.APP_DATABASE_NAME))
                        File.Delete(AppConst.APP_DATABASE_NAME);
                    worked = true;
                }
                catch (IOException e)   // delete only throws this on locking
                {
                    Console.WriteLine("Delete failed cause: "+ e.Message);
                    Console.WriteLine("Retry...");
                    tries++;
                }
            }
            if (!worked)
                throw new IOException("Unable to close file " + AppConst.APP_DATABASE_NAME);
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
            connection.Dispose();
        }

        public int InitTables()
        {
            string sql = "create table users (name text, hash BLOB, pass BLOB)";

            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            int result = command.ExecuteNonQuery();

            CloseDataBase();

            return result;
        }

        public int AddUser(string name, byte[] hash, byte[] pass)
        {
            string hashInString = System.Text.Encoding.ASCII.GetString(hash);
            string sql = "INSERT INTO users (name, hash, pass) VALUES (@Name, @Hash, @Pass)";


            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.Add("@Name", System.Data.DbType.String).Value = name;
            command.Parameters.Add("@Hash", System.Data.DbType.Object, hash.Length).Value = hash;
            command.Parameters.Add("@Pass", System.Data.DbType.Object, pass.Length).Value = pass;
            int result = command.ExecuteNonQuery();

            CloseDataBase();

            return result;
        }

        public DataBaseUser GetUser(string name)
        {
            string sql = "SELECT * FROM users WHERE name=@Name";
            DataBaseUser user;
            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.Add("@Name", System.Data.DbType.String).Value = name;
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            user = new DataBaseUser {
                Name = (string)reader["name"],
                Hash = (byte[])reader["hash"],
                Pass = (byte[])reader["pass"]
            };
            CloseDataBase();
            return user;
        }

        public bool CheckIfUserExist(string name)
        {
            string sql = "SELECT name, hash, pass FROM users WHERE name=@Name";
            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.Add("@Name", System.Data.DbType.String).Value = name;
            SQLiteDataReader reader = command.ExecuteReader();
            bool hasData = reader.HasRows;
            CloseDataBase();
            return hasData;
        }

        public List<DataBaseUser> ReadAll()
        {
            List<DataBaseUser> users = new List<DataBaseUser>();
            string sql = "SELECT name, hash, pass FROM users ORDER BY name desc";

            OpenDataBase();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Debug.WriteLine("Getting All Users");
            while (reader.Read()) {
                DataBaseUser user = new DataBaseUser {
                    Name = (string)reader["name"],
                    Hash = (byte[])reader["hash"],
                    Pass = (byte[])reader["pass"]
                };
                users.Add(user);
                Debug.WriteLine(user);
            }

            CloseDataBase();
            return users;
        }
    }
}
