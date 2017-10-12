using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class LoginControlViewModel: BaseViewModel
    {
        #region Content Language
        public string SignInText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("signIn_lbl");
        }

        public string DoesntHaveAccountText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("doesntHaveAccount_lbl");
        }

        #endregion

        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand CreateNewAccountCommand { get; set; }
        #endregion

        #region Constructor
        public LoginControlViewModel()
        {
            InitCommands();
        }
        #endregion

        #region Private Methods

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(() => Login());
            CreateNewAccountCommand = new RelayCommand(() => GoToCreateForm());
        }

        private void Login()
        {
            System.Console.WriteLine("LOG IN SUCCESSFULL");
            throw new System.NotImplementedException("This metohds is in progress...");
        }

        private void GoToCreateForm()
        {
            throw new System.NotImplementedException("This metohds is in progress...");
        }

        #endregion
    }
}
