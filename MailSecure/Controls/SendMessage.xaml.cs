using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using MailSecure.Security;
using MailSecure.Core;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour SendMessage.xaml
    /// </summary>
    public partial class SendMessage : UserControl
    {
        MailSender mailSender;
        private string fileToEncrypt;

        public SendMessage()
        {
            InitializeComponent();
            mailSender = App.mailSender;
            DataContext = App.CurrentUserData;
        }

        private void sendBtn(object sender, RoutedEventArgs e)
        {
            string randomPassword = Utils.RandomPassword(8);
            string to = this.toTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string messageCryted = Encryption.Encrypt(this.messageTextBox.Text.ToString(), randomPassword);
            string body = messageCryted;

            MailMessage mail = new MailMessage(App.CurrentUserData.CurrentUser.email, to, subject, body);
            if (string.IsNullOrEmpty(fileToEncrypt) == false)
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileLabel.Content;
                string destPath = fileLabel.Content + ".lock";
                FileEncryption fileEncryption = new FileEncryption();

                fileEncryption.EncryptFile(fileToEncrypt, destPath, randomPassword);
                mail.Attachments.Add(new Attachment(destPath));
            }
            mailSender.setMailMessage(mail);
            mailSender.setCurrentUser(App.CurrentUserData.CurrentUser);
            
            mailSender.SendMail();

            var passwordPopup = new PasswordPopup();
            passwordPopup.passwordGeneratedLabel.Content = "Password généré : " + randomPassword;
            passwordPopup.Show();
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            MailServerConfigurationWindow mailConf = new MailServerConfigurationWindow();
            mailConf.ShowDialog();
            //login.Show();
        }

        private void attachmentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileToLoad = new OpenFileDialog();
            fileToLoad.Filter = "All Files (*.*)|*.*";
            fileToLoad.FilterIndex = 1;
            fileToLoad.Multiselect = false;

            if (fileToLoad.ShowDialog() == true)
            {
                fileToEncrypt = fileToLoad.FileName;
                fileLabel.Content = Utils.GetFileNameFromPath(fileToLoad.FileName);
            }
        }
    }
}
