using System.IO;
using System;

namespace MailSecure.Core
{
    public class BinaryMCSFileManager
    {
        public static bool WriteStructInFile(UserMailFacts facts)
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;

            CreateFolderAndFile();

            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        writer.Write(facts.UserName);
                        writer.Write(facts.EmailAdress);
                        writer.Write(facts.Login);
                        writer.Write(facts.SmtpAdress);
                        writer.Write(facts.ImapAdress);
                        writer.Write(facts.EncodingText.Length);
                        writer.Write(facts.EncodingText);
                        writer.Write(facts.Entropy);
                    }
                }

                return true;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }

            return false;
        }

        public static UserMailFacts ReadStructInFile()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;
            UserMailFacts userFacts = new UserMailFacts();
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                using(BinaryReader reader = new BinaryReader(fileStream))
                {
                    userFacts.UserName = reader.ReadString();
                    userFacts.EmailAdress = reader.ReadString();
                    userFacts.Login = reader.ReadString();
                    userFacts.SmtpAdress = reader.ReadString();
                    userFacts.ImapAdress = reader.ReadString();
                    int count = reader.ReadInt32();
                    userFacts.EncodingText = reader.ReadBytes(count);
                    userFacts.Entropy = reader.ReadBytes(20);
                }
            }

            return userFacts;
        }

        public static bool CheckIfConfigFileExist()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;

            return File.Exists(filePath);
        }

        public static bool IsFileEmpty()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;

            if (new FileInfo(filePath).Length == 0) {
                return true;
            }

            return false;
        }

        public static bool CheckIfFolderExist()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);

            return Directory.Exists(folderPath);

        }

        public static void CreateConfigFolder()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);

            try
            {
                Directory.CreateDirectory(folderPath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void CreateConfigFile()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;

            try
            {
                FileStream file = File.Create(filePath);
                file.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void CreateFolderAndFile()
        {
            if(!CheckIfFolderExist())
            {
                CreateConfigFolder();
            }

            if(!CheckIfConfigFileExist())
            {
                CreateConfigFile();
            }
        }
    }
}
