using System.Windows;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application {
        public static MailSender mailSender { get; set; }
        public static UserMailFacts currentUser { get; set; }

        public App() {
            mailSender = new MailSender(ServerFactConst.DEFAULT_SMTP_PORT_TLS);

        }
    }
}
