using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour UnlockPage.xaml
    /// </summary>
    public partial class UnlockPage : Page
    {
        public UnlockPage()
        {
            App.UnlockPageViewModel = new UnlockPageViewModel();
            DataContext = App.UnlockPageViewModel;
            InitializeComponent();         
        }
    }
}
