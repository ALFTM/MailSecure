using System;
using System.Windows;
using System.Net;
using System.Net.Mail;


namespace MailSecure {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = App.mailSender;
        }

        private void sendBtn(object sender, RoutedEventArgs e) {
            string to = this.toTextBox.Text.ToString();
            string from = this.fromTextBox.Text.ToString();
            string subject = this.objectTextBox.Text.ToString();
            string body = this.messageTextBox.Text.ToString();

            MailMessage mail = new MailMessage(from, to, subject, body);
            App.mailSender.setMailMessage(mail);

            App.mailSender.SendMail();

            

        }

        private void loginBtn(object sender, RoutedEventArgs e) {
            Login login = new Login();
            login.Show();
        }
    }
}
