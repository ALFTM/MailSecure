using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSecure.UserControls
{
    /// <summary>
    /// Logique d'interaction pour SendMessage.xaml
    /// </summary>
    public partial class SendMessage : UserControl
    {
        public SendMessage()
        {
            InitializeComponent();
            DataContext = App.mailSender;
        }

        private void sendBtn(object sender, RoutedEventArgs e)
        {
            string to = this.toTextBox.Text.ToString();
            string from = this.fromTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string body = this.messageTextBox.Text.ToString();

            MailMessage mail = new MailMessage(from, to, subject, body);
            App.mailSender.setMailMessage(mail);

            App.mailSender.SendMail();
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            MailServerConfigurationWindow mailConf = new MailServerConfigurationWindow();
            mailConf.ShowDialog();
            //login.Show();
        }
    }
}
