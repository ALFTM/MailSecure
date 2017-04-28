using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MailSecure {
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application {
        public static MailSender mailSender { get; set; }

        public App() {
            mailSender = new MailSender("smtp-mail.outlook.com", 587);
        }
    }
}
