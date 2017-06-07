using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace MailSecure.UserControls
{
    /// <summary>
    /// Logique d'interaction pour SendMessage.xaml
    /// </summary>
    public partial class SendMessage : UserControl
    {
        MailSender mailSender;
        public SendMessage()
        {
            InitializeComponent();
            mailSender = App.mailSender;
            DataContext = App.CurrentUserData;
        }

        private void sendBtn(object sender, RoutedEventArgs e)
        {
            string to = this.toTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string body = this.messageTextBox.Text.ToString();

            MailMessage mail = new MailMessage(App.CurrentUserData.CurrentUser.email, to, subject, body);
            mailSender.setMailMessage(mail);
            mailSender.setCurrentUser(App.CurrentUserData.CurrentUser);

            mailSender.SendMail();
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
