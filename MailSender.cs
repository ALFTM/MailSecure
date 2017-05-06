
using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace MailSecure {

    public class MailSender {
        public string FromLabel { get { return mailSenderLangMngr.GetStringFromLanguage("from_lbl"); } }
        public string ToLabel { get { return mailSenderLangMngr.GetStringFromLanguage("to_lbl"); } }
        public string ObjectLabel { get { return mailSenderLangMngr.GetStringFromLanguage("object_lbl"); } }
        public string MesageLabel { get { return mailSenderLangMngr.GetStringFromLanguage("mesage_lbl"); } }
        public string SendButton { get { return mailSenderLangMngr.GetStringFromLanguage("send_lbl"); } }
        public string server { get; set; }
        public int port { get; set; }
        public bool canSend { get; set; }

        private LanguageManager mailSenderLangMngr;
        private SecureString password;
        private SmtpClient smtpClient;
        private MailMessage mailMessage;

        public MailSender() {
            canSend = false;
            mailSenderLangMngr = LanguageManager.GetInstance;
        }

        public MailSender(string server, int port) {
            this.server = server;
            this.port = port;
            canSend = false;
            mailSenderLangMngr = LanguageManager.GetInstance;
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

