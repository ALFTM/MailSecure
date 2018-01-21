using MailSecure.Security;
using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, IHavePassword
    {
        public LoginControl()
        {
            InitializeComponent();
            PrepareForm();
            DataContext = new LoginViewModel(this);
        }

        private void PrepareForm()
        {
            this.loginId.Focus();
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }
    }
}
