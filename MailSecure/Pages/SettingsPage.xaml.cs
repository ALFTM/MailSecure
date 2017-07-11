using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
            DataContext = new SettingsPageViewModel();
        }

        private void Language_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshContent();
        }

        private void RefreshContent()
        {
            AddAcountButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
            SettingText.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            var parentWindow = Window.GetWindow(this) as FlatWindow;
            if (parentWindow != null) {
                var sideMenu = parentWindow.SideMenuUC;
                sideMenu.SendMenuButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
                sideMenu.UnlockMenuButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
                sideMenu.SettingButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
                sideMenu.AboutButton.GetBindingExpression(Button.ContentProperty).UpdateTarget();
            }
        }
    }
}
