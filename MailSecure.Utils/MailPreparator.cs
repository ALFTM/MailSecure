using System;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Security;
using System.Text;
using MailSecure.Core;

namespace MailSecure.Security
{
    public static class MailPreparator
    {
        public static MailMessage GetEncryptedMail(string body, string password, string user, SecureString pass)
        {
            string messageCryted = Encryption.Encrypt(body, password);

            MailMessage mail = new MailMessage()
            {
                Body = messageCryted
            };
            return mail;
        }

        public static MailMessage GetEncryptedMail(string body, string password, Collection<string> attachmentsList, string user, SecureString pass)
        {
            // string messageCryted = Encryption.Encrypt(body, password);

            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER_EXTERNAL_KEY);
            string currentUserPath = folderPath + "\\" + user;
            string pubKey = currentUserPath + "\\pubring.gpg";

            PgpEncryptionKeys keys = new PgpEncryptionKeys(pubKey);

            PgpCypher pgpCypher = new PgpCypher(keys);

            var encryptedBody = pgpCypher.Encrypt(Encoding.UTF8.GetBytes(body));

            MailMessage mail = new MailMessage()
            {
                Body = Encoding.UTF8.GetString(encryptedBody)
            };
          
            mail.Headers.Add("MailSecure", "MailSecure_Crypt");

            AddEncryptedFiled(ref mail, ref attachmentsList, ref password);

            return mail;
        }

        private static void AddEncryptedFiled(ref MailMessage mail, ref Collection<string> list, ref string password)
        {
            DirectoryManager.CreateTempFolder();
            for(int i = 0; i < list.Count; i++) {
                var item = list[i];
                string filePath = item;
                string destPath = DirectoryManager.tempfolderPath + Utils.GetFileNameFromPath(item) + ".lock";

                IFileEncryption fileEncryption = new FileEncryptionCBC();

                fileEncryption.EncryptFile(filePath, destPath, password);
                mail.Attachments.Add(new Attachment(destPath));
            }
        }

        public static string GetPGPBody(string body, string user, SecureString pass)
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER_KEY);
            string currentUserPath = folderPath + "\\" + user;
            string pubKey = currentUserPath + "\\pubring.gpg";
            string secretKey = currentUserPath + "\\secring.gpg";

            PgpEncryptionKeys keys = new PgpEncryptionKeys(pubKey, secretKey, Utils.ConvertToUnsecureString(pass));

            PgpCypher pgpCypher = new PgpCypher(keys);

            var decryptedBody = pgpCypher.Decrypt(Encoding.UTF8.GetBytes(body));

            return Encoding.UTF8.GetString(decryptedBody);
        }
    }
}
