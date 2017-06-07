using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using System.Windows.Controls;
using MailSecure.UserControls;

namespace MailSecure {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            ChangeViewContent(new SendMessage());
        }

        private void Option_UC(object sender, RoutedEventArgs e)
        {
            Option optionUC = new Option();
            ChangeViewContent(optionUC);
        }

        private void SendMessage_UC(object sender, RoutedEventArgs e)
        {
            SendMessage sendMessageUC = new SendMessage();
            ChangeViewContent(sendMessageUC);
        }

        private void DecryptMessage_UC(object sender, RoutedEventArgs e)
        {
            DecryptMessage decryptMessageUC = new DecryptMessage();
            ChangeViewContent(decryptMessageUC);
        }

        public void ChangeViewContent(UserControl uc)
        {
            GridContent.Children.Clear();
            GridContent.Children.Add(uc);
        }

        private void ReadDecrypt_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
