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
            InitializeComponent();
            DataContext = new UnlockPageViewModel();
        }
    }
}
