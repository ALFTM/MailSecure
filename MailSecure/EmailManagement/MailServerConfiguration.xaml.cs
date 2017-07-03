using System.Windows;
using System.Security.Cryptography;


namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class MailServerConfigurationWindow : Window
    {
        public MailServerConfigurationWindow()
        {
            InitializeComponent();
        }

        private void SaveConfiguration(object sender, RoutedEventArgs e)
        {
            UserMailFacts userFacts = new UserMailFacts();

            this.CryptPassword(ref userFacts);

            userFacts.userName = this.userNameTextBox.Text;
            userFacts.login = this.loginTextBox.Text;
            userFacts.smtpAdress = this.smtpServerTextBox.Text;
            userFacts.email = this.userEmailTextBox.Text;
            
            bool res = BinaryMCSFileManager.WriteStructInFile(userFacts);

            if(res)
            {
                App.CurrentUserData.CurrentUser = userFacts;
                App.CurrentUserData.DisplayedName = userFacts.userName;
            }

            this.Close();
        }

        public void CryptPassword(ref UserMailFacts userFacts)
        {
            byte[] plainText = System.Text.Encoding.UTF8.GetBytes(this.passwordPasswordBox.Password);

            // Generate additional entropy (will be used as the Initialization vector)
            byte[] entropy = new byte[20];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(entropy);

            byte[] encodeText = ProtectedData.Protect(plainText, entropy, DataProtectionScope.CurrentUser);

            plainText.Initialize();

            userFacts.entropy = entropy;
            userFacts.encodingText = encodeText;
        }
    }
}