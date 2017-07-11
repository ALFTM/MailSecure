using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour SendingPage.xaml
    /// </summary>
    public partial class SendingPage : Page
    {
        public SendingPage()
        {
            App.SendingPageViewModel = new SendingPageViewModel();
            InitializeComponent();
            DataContext = App.SendingPageViewModel;
        }
    }
}
