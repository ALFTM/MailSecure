using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
            DataContext = new LoginControlViewModel();
            PrepareForm();
        }

        private void PrepareForm()
        {
            this.loginId.Focus();
        }
    }
}
