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
        UserMailFacts currentUser;
        MailSender mailSender;
        public SendMessage()
        {
            InitializeComponent();
            mailSender = App.mailSender;
            this.getCurrentUser();
            DataContext = currentUser;
        }

        private void sendBtn(object sender, RoutedEventArgs e)
        {
            string to = this.toTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string body = this.messageTextBox.Text.ToString();

            MailMessage mail = new MailMessage(currentUser.email, to, subject, body);
            mailSender.setMailMessage(mail);
            mailSender.setCurrentUser(currentUser);

            mailSender.SendMail();
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            MailServerConfigurationWindow mailConf = new MailServerConfigurationWindow();
            mailConf.ShowDialog();
            //login.Show();
        }

        private void getCurrentUser()
        {
            if(BinaryMCSFileManager.CheckIfConfigFileExist())
            {
                currentUser = BinaryMCSFileManager.ReadStructInFile();
            }
        }
    }
}
