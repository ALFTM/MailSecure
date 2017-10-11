using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class LoginWindowViewModel: BaseViewModel
    {
        #region Private Members
        #endregion

        #region Public Members
        #endregion

        #region Content Language
        public string SignInText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("signIn_lbl");
        }

        #endregion

        #region Commands
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginWindowViewModel()
        {
            LoginCommand = new RelayCommand(() => Login());
        }
        #endregion

        private void Login()
        {
            System.Console.WriteLine("LOG IN SUCCESSFULL");
        }
    }
}
