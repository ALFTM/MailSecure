using MailSecure.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
    }
}
