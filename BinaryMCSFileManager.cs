﻿using System.IO;
using System;

namespace MailSecure
{
    class BinaryMCSFileManager
    {
        public static void WriteStructInFile(UserMailFacts facts)
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;
            FileStream fileStream;
            BinaryWriter writer;

            Directory.CreateDirectory(folderPath);


            try
            {

                fileStream = File.Open(filePath, FileMode.Open);
                writer = new BinaryWriter(fileStream);

                writer.Write(facts.userName);
                writer.Write(facts.email);
                writer.Write(facts.login);
                writer.Write(facts.smtpAdress);
                writer.Write(facts.encodingText.Length);
                writer.Write(facts.encodingText);
                writer.Write(facts.entropy);

                fileStream.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static UserMailFacts ReadStructInFile()
        {
            string filePath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            FileStream fileStream;
            BinaryReader reader;
            UserMailFacts userFacts = new UserMailFacts();

            fileStream = File.Open(filePath, FileMode.Open);
            reader = new BinaryReader(fileStream);

            userFacts.userName = reader.ReadString();
            userFacts.email = reader.ReadString();
            userFacts.login = reader.ReadString();
            userFacts.smtpAdress = reader.ReadString();
            int count = reader.ReadInt32();
            userFacts.encodingText = reader.ReadBytes(count);
            userFacts.entropy = reader.ReadBytes(20);

            return userFacts;
        }

        public static bool CheckIfConfigFileExist()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER);
            string filePath = folderPath + "\\" + AppConst.USER_CONFIG_FILE_NAME;

            return File.Exists(filePath);
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
                File.Create(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}