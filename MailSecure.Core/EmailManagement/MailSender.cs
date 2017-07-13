
using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Security.Cryptography;

namespace MailSecure.Core {

    public class MailSender {
        public string server { get; set; }
        public int port { get; set; }
        public bool canSend { get; set; }

        private SecureString password;
        private SmtpClient smtpClient;
        private MailMessage mailMessage;
        private UserMailFacts currentUser;

        public MailSender() {
            canSend = false;
        }

        public MailSender(int port) {
            this.server = server;
            this.port = port;
            canSend = false;
        }

        public void setMailMessage(MailMessage message) {
            this.mailMessage = message;
        }

        public void setCurrentUser(UserMailFacts currentUser)
        {
            this.currentUser = currentUser;
        }

        public void setCredentials(SecureString password) {
            this.password = password;
            canSend = true;
        }

        public void SendMail() {

            this.prepareSmtp();

            try {
                smtpClient.Send(mailMessage);
                canSend = false;

                smtpClient = null;
            }
            catch (Exception ex) {
                Console.WriteLine(
                "Exception caught in CreateTestMessage1(): {0}",
                ex.ToString());
            }
        }

        private void prepareSmtp() {
            smtpClient = new SmtpClient(currentUser.SmtpAdress, port)
            {
                EnableSsl = true
            };
            this.setCredentials();
        }

        private void setCredentials()
        {
            byte[] plainText = ProtectedData.Unprotect(currentUser.EncodingText, currentUser.Entropy, DataProtectionScope.CurrentUser);
            System.Text.Encoding.UTF8.GetString(plainText).ToString();

            smtpClient.Credentials = new NetworkCredential(currentUser.Login, System.Text.Encoding.UTF8.GetString(plainText).ToString());

        }

    }
}

