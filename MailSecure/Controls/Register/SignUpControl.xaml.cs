using MailSecure.Security;
using MailSecure.ViewModel;
using System.Windows.Controls;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl, IHavePassword
    {
        public SignUpControl()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(this);
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
