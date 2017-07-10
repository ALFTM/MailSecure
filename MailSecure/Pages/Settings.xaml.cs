using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        public Settings()
        {
            InitializeComponent();            
            DataContext = new SettingViewModel();
        }

        private void Language_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshContent();
        }

        private void RefreshContent()
        {
            AddAcountButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
        }
    }
}
