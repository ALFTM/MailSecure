using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour UnlockControl.xaml
    /// </summary>
    public partial class UnlockControl : UserControl
    {
        public UnlockControl()
        {
            InitializeComponent();
            App.UnlockPageViewModel.Control = this;
        }
    }
}
