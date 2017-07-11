using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
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
