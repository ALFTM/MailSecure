﻿using System.Net.Mail;
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
            this.getCurrentUser();
            DataContext = App.CurrentUserData;
        }

        private void sendBtn(object sender, RoutedEventArgs e)
        {
            string to = this.toTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string messageCryted = Encryption.Encrypt(this.messageTextBox.Text.ToString(), "password");
            string body = messageCryted;

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

        private void getCurrentUser()
        {
            if(BinaryMCSFileManager.CheckIfConfigFileExist())
            {
                App.CurrentUserData.CurrentUser = BinaryMCSFileManager.ReadStructInFile();
                App.CurrentUserData.DisplayedName = App.CurrentUserData.CurrentUser.userName;
            }
        }
    }
}
