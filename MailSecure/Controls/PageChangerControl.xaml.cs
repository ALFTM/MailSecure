using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour PageChangerControl.xaml
    /// </summary>
    public partial class PageChangerControl : UserControl
    {
        public PageChangerControl()
        {
            InitializeComponent();
            DataContext = App.CurrentUserData;
        }
    }
}
