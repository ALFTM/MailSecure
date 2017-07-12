using System.Collections.ObjectModel;
using System.Net.Mail;
using MailSecure.Core;

namespace MailSecure.Security
{
    public static class MailPreparator
    {
        public static MailMessage GetEncryptedMail(string body, string password)
        {
            string messageCryted = Encryption.Encrypt(body, password);

            MailMessage mail = new MailMessage()
            {
                Body = messageCryted
            };
            return mail;
        }

        public static MailMessage GetEncryptedMail(string body, string password, Collection<string> attachmentsList)
        {
            string messageCryted = Encryption.Encrypt(body, password);

            MailMessage mail = new MailMessage()
            {
                Body = messageCryted
            };
            AddEncryptedFiled(ref mail, ref attachmentsList, ref password);

            return mail;
        }

        private static void AddEncryptedFiled(ref MailMessage mail, ref Collection<string> list, ref string password)
        {
            DirectoryManager.CreateTempFolder();
            for(int i = 0; i < list.Count; i++) {
                var item = list[i];
                string filePath = item;
                string destPath = "./tmp/" + Utils.GetFileNameFromPath(item) + ".lock";

                FileEncryption fileEncryption = new FileEncryption();

                fileEncryption.EncryptFile(filePath, destPath, password);
                mail.Attachments.Add(new Attachment(destPath));
            }
        }
    }
}
