
using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace MailSecure {

    public class MailSender {

        public string server { get; set; }
        public int port { get; set; }
        public bool canSend { get; set; }

        private SecureString password;
        private SmtpClient smtpClient;
        private MailMessage mailMessage;

        public MailSender() {
            canSend = false;
        }

        public MailSender(string server, int port) {
            this.server = server;
            this.port = port;
            canSend = false;
        }

        public void setMailMessage(MailMessage message) {
            this.mailMessage = message;
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
            }
            catch (Exception ex) {
                Console.WriteLine(
                "Exception caught in CreateTestMessage1(): {0}",
                ex.ToString());
            }
        }

        private void prepareSmtp() {
            smtpClient = new SmtpClient(server, port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(mailMessage.From.Address, this.password);
        }

    }
}

