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
using System.Windows.Shapes;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour PasswordPopup.xaml
    /// </summary>
    public partial class PasswordPopup : Window
    {
        public PasswordPopup()
        {
            InitializeComponent();
            DataContext = new PasswordPopupViewModel();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void copiedButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(passwordGeneratedLabel.Content.ToString().Substring(passwordGeneratedLabel.Content.ToString().IndexOf(':')+2));
            MessageBox.Show("Mot de passe copié !");
        }
    }
}