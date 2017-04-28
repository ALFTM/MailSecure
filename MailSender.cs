
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace MailSecure {

    public class MailSender {

        private string server;
        private int port;

        public MailSender() {

        }

        public MailSender(string server, int port) {
            this.server = server;
            this.port = port;
        }

        public void SendMail() {
            string to = "maxime.pelte@gmail.com";
            string from = "maxime.pelte@outlook.fr";
            string subject = "Using the new SMTP client.";
            string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient(server, port);
            client.EnableSsl = true;
            // Credentials are necessary if the server requires the client 
            // to authenticate before it will send e-mail on the client's behalf.
            client.Credentials = new NetworkCredential(from, "test");

            try {
                client.Send(message);
            }
            catch (Exception ex) {
                Console.WriteLine(
                "Exception caught in CreateTestMessage1(): {0}",
                ex.ToString());
            }

        }
    }
}

