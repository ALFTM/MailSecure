using System.Windows;
using System.Security.Cryptography;
using MailSecure.Core;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class MailServerConfigurationWindow : Window
    {
        public MailServerConfigurationWindow()
        {
            InitializeComponent();
            DataContext = new ServerConfigurationViewModel(this);
        }

        public MailServerConfigurationWindow(string title, string smtp)
        {
            InitializeComponent();
            DataContext = new ServerConfigurationViewModel(this, title, smtp);
        }
    }
}