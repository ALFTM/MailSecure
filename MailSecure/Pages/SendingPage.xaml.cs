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
            InitializeComponent();
            DataContext = new SendingPageViewModel();
        }
    }
}
